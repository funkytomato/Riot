﻿using Eliot.Environment;
using UnityEngine;

namespace Eliot.AgentComponents
{
    /// <summary>
    /// Physical keeper of the Spell casted by an Agent. Moves forward in
    /// specified direction after being cast untill it collides with another
    /// physical object. Perfectly fits for fireballs, bullets etc.
    /// </summary>
    [RequireComponent(typeof(Unit))]
    public class ProjectileNoMovement : MonoBehaviour
    {

        public bool isTriggered = false;

        [Header("Grenade Thrower")]
        public float m_GrenadeRange = 30.0f;
        public float m_Gravity = -9.80665f;

        private Vector3 m_CurrentVelocity = new Vector3(1.0f, 0.0f, 0.0f);
        private float m_CurrentDesiredAngle = 0.0f;
        private float m_CurrentDesiredSpeed = 0.0f;

        [SerializeField] private bool _canAffectEnemies = true;
        [SerializeField] private bool _canAffectFriends = true;
        [SerializeField] private int _minDamage;
        [SerializeField] private int _maxDamage;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private bool _chaseTarget;
        [SerializeField] private float _rotationSpeed = 5f;
        [SerializeField] private bool _detachChildren;
        [SerializeField] private bool _destroyOnAnyCollision;
        [Space]
        [Tooltip("If true, a method with specified name will be invoked on an object" +
                 " that projectile colides with. Random number between MinDamage" +
                 " and MaxDamage will be passed as a parameter.")]
        [SerializeField] private bool _sendDamageMessage = true;
        [SerializeField] private string _damageMethodName = "Damage";
        [Space]
        [Tooltip("Other methods to be invoked without parameters on a collision object.")]
        [SerializeField] private string[] _messages;
        [Space]
        [Tooltip("Skill that will be applied to an Agent the Projectile collides with.")]
        [SerializeField] private Skill _skill;
        /// The caster of the Projectile.
        private Agent _owner;
        /// The target of the Projectile.
        private Transform _target;
        /// Time at which the Projectile was cast.
        private float _initTime;

        /// <summary>
        /// Initialise Projectile's components.
        /// </summary>
        /// <param name="agent"></param>
        /// <param name="target"></param>
        /// <param name="skill"></param>
        /// <param name="minDamage"></param>
        /// <param name="maxDamage"></param>
        /// <param name="canAffectEnemies"></param>
        /// <param name="canAffectFriends"></param>
        public void Init(Agent agent, Transform target, Skill skill, int minDamage = 0, int maxDamage = 5,
            bool canAffectEnemies = true, bool canAffectFriends = true)
        {
            _canAffectEnemies = canAffectEnemies;
            _canAffectFriends = canAffectFriends;
            _owner = agent;
            _target = target;
            _skill = skill.Clone();
            _skill.Init(_owner, _owner.gameObject);
            _initTime = Time.time;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
        }



        private void Start()
        {
            m_CurrentDesiredAngle = 0.0f;
            m_CurrentDesiredSpeed = 12.0f;


            Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);
            Vector3 bankingAxis = new Vector3(0.0f, 1.0f, 0.0f);
            direction = Quaternion.AngleAxis(m_CurrentDesiredAngle, bankingAxis) * direction;
            Vector3 horizonAxis = Vector3.Cross(direction, bankingAxis);
            direction = Quaternion.AngleAxis(45.0f, horizonAxis) * direction;
            Vector3 startVelocity = direction * m_CurrentDesiredSpeed;

            // spawn a grenade
            //GameObject temp = GameObject.Instantiate(m_GrenadeObject);
            //temp.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            ThrownProjectileScript grenade = gameObject.GetComponent<ThrownProjectileScript>();

            grenade.m_Gravity = m_Gravity;
            grenade.m_CurrentVelocity = startVelocity;
            grenade.enabled = true;
        }

        /// <summary>
        /// Update object every frame.
        /// </summary>
        private void Update()
        {
            //if (_chaseTarget && _target)
            //{
            //	var targetPos = _target.position;
            //	if (_target.GetComponent<Unit>() && _target.GetComponent<Unit>().Type == UnitType.Agent)
            //		targetPos.y += _target.localScale.y / 2;
            //	var targetDir = targetPos - transform.position;
            //	var step = _rotationSpeed * Time.deltaTime;
            //	var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            //	transform.rotation = Quaternion.LookRotation(newDir);
            //}
            //transform.position += transform.forward * _speed;
            //if(Time.time >= _initTime + _lifeTime) Destroy(gameObject);



            //Don't need to do this because calling the script

            //float dt = Time.deltaTime;
            //Vector3 position = transform.position;
            //ProjectileHelper.UpdateProjectile(ref position, ref m_CurrentVelocity, m_Gravity, dt);
            //transform.position = position;

            //if (position.y < 0.0f)
            //{
            //    GameObject.Destroy(this.gameObject);
            //}
        }

        /// <summary>
        /// Pass all needed information to the object on collision.
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            var agent = other.gameObject.GetComponent<Agent>();

            var damage = Random.Range(_minDamage, _maxDamage + 1);
            if (!agent && _sendDamageMessage) other.gameObject.SendMessage(
                _damageMethodName, damage, SendMessageOptions.DontRequireReceiver);
            foreach (var message in _messages)
                other.gameObject.SendMessage(message, SendMessageOptions.DontRequireReceiver);

            if (agent && !agent.Equals(_owner) && !agent.Inert)
            {
                var targetIsFriend = _owner.Unit.IsFriend(agent.Unit);
                if (targetIsFriend && !_canAffectFriends) return;
                if (!targetIsFriend && !_canAffectEnemies) return;

                if (_skill != null) agent.AddEffect(_skill, _owner);
                else agent.Resources.Damage(damage);
            }

            if (agent && !agent.Equals(_owner))
            {
                if (_detachChildren)
                {
                    foreach (Transform child in transform)
                        child.parent = null;
                }
                Destroy(gameObject);
            }

            if (!agent && (other.gameObject.GetComponent<Unit>() && (other.gameObject.GetComponent<Unit>().Type != UnitType.Projectile)))
                Destroy(gameObject);

            if (_destroyOnAnyCollision)
                Destroy(gameObject);
        }

    }
}