using System;
using System.Collections;
using System.Collections.Generic;
using Eliot.Utility;
using UnityEngine;

namespace Eliot.AgentComponents
{
	/// <summary>
	/// Keep information about finite resources (HP, Energy etc.).
	/// Resources usage is optional.
	/// </summary>
	[Serializable]
	public class Resources
	{
		/// <summary>
		/// Maximum amount of health that the Agent can have.
		/// </summary>
		public int MaxHealthPoints
		{
			get { return _healthPoints; }
		}

		/// <summary>
		/// Maximum amount of energy that the Agent can have.
		/// </summary>
		public int MaxEnergyPoints
		{
			get { return _energyPoints; }
		}

        //Additional Resources
        /// <summary>
        /// Maximum amount of intoxication that the Agent can have.
        /// </summary>
        public int MaxIntoxicationPoints
        {
            get { return _intoxicationPoints; }
        }

        //Additional Resources
        /// <summary>
        /// Maximum amount of morale that the Agent can have.
        /// </summary>
        public int MaxMoralePoints
        {
            get { return _moralePoints; }
        }

        //Additional Resources
        /// <summary>
        /// Maximum amount of temperament that the Agent can have.
        /// </summary>
        public int MaxTemperamentPoints
        {
            get { return _temperamentPoints; }
        }

        /// <summary>
        /// Current amount of Agent's health.
        /// </summary>
        public int HealthPoints
		{
			get { return _curHealthPoints; }
		}

		/// <summary>
		/// Wheather the agent uses health as a resource.
		/// </summary>
		public bool UsesHealth
		{
			get { return _useHealth; }
		}

        /// <summary>
        /// Current amount of Agent's energy.
        /// </summary>
        public int EnergyPoints
        {
            get { return _curEnergyPoints; }
        }

        /// <summary>
        /// Wheather the agent uses energy as a resource.
        /// </summary>
        public bool UsesEnergy
		{
			get { return _useEnergy; }
		}

        /// <summary>
        /// Current amount of Agent's intoxication.
        /// </summary>
        public int IntoxicationPoints
        {
            get { return _curIntoxicationPoints; }
        }

        /// <summary>
        /// Wheather the agent uses intoxication as a resource.
        /// </summary>
        public bool UsesIntoxication
        {
            get { return _useIntoxication; }
        }

        /// <summary>
        /// Current amount of Agent's morale.
        /// </summary>
        public int MoralePoints
        {
            get { return _curMoralePoints; }
        }

        /// <summary>
        /// Wheather the agent uses morale as a resource.
        /// </summary>
        public bool UsesMorale
        {
            get { return _useMorale; }
        }


        /// <summary>
        /// Current amount of Agent's temperament.
        /// </summary>
        public int TemperamentPoints
        {
            get { return _curTemperamentPoints; }
        }

        /// <summary>
        /// Wheather the agent uses temperament as a resource.
        /// </summary>
        public bool UsesTemperament
        {
            get { return _useTemperament; }
        }

        /// <summary>
        /// Returns wheather the user wants to dubug the Resources configuration.
        /// </summary>
        public bool Debug
		{
			get { return _debug; }
		}

		[Header("Health")] [SerializeField] private bool _useHealth;
		[SerializeField] private int _healthPoints = 10;
		[SerializeField] private int _curHealthPoints;
		[SerializeField] private bool _healOverTime = true;
		[SerializeField] private int _healAmount = 1;
		[SerializeField] private float _healCoolDown = 5;
		[Space]
		[Header("Energy")] [SerializeField] private bool _useEnergy;
		[SerializeField] private int _energyPoints = 10;
		[SerializeField] private int _curEnergyPoints;
		[SerializeField] private bool _addEnergyOverTime = true;
		[SerializeField] private int _addEnergyAmount = 1;
		[SerializeField] private float _addEnergyCoolDown = 5;
		[Space]
        [Header("Intoxication")] [SerializeField] private bool _useIntoxication;
        [SerializeField] private int _intoxicationPoints = 10;
        [SerializeField] private int _curIntoxicationPoints;
        [SerializeField] private bool _addIntoxicationOverTime = true;
        [SerializeField] private int _addIntoxicationAmount = 1;
        [SerializeField] private float _addIntoxicationCoolDown = 5;
        [Header("Morale")] [SerializeField] private bool _useMorale;
        [SerializeField] private int _moralePoints = 10;
        [SerializeField] private int _curMoralePoints;
        [SerializeField] private bool _addMoraleOverTime = true;
        [SerializeField] private int _addMoraleAmount = 1;
        [SerializeField] private float _addMoraleCoolDown = 5;
        [Header("Temperament")] [SerializeField] private bool _useTemperament;
        [SerializeField] private int _temperamentPoints = 10;
        [SerializeField] private int _curTemperamentPoints;
        [SerializeField] private bool _addTemperamentOverTime = true;
        [SerializeField] private int _addTemperamentAmount = 1;
        [SerializeField] private float _addTemperamentCoolDown = 5;
        [Space]
        [Header("Other")] 
		[SerializeField] private List<AudioClip> _onDamageSounds;
		[Tooltip("Duration for which Agent's movement and skills will be locked upon taking damage.")]
		[SerializeField] private float _lockTime = 0.3f;
		[Space]
		[Tooltip("Configuations of the actions to be taken upon Agent's death.")]
		[SerializeField] public Death Death;
		
		[Space(10)] 
		[Tooltip("Display the Resources configuration in editor?")]
		[SerializeField] private bool _debug = true;

		/// A link to actual controller.
		private Agent _agent;
		/// A link to Animation component of the Agent.
		private AgentAnimation _agentAnimation;
		/// Condition Interfaces of this instance of Agent's Resources component.
		private List<ResourcesConditionInterface> _conditionInterfaces = new List<ResourcesConditionInterface>();
		/// Last time the Agent automatically self-healed.
		private float _lastTimeHealed;
		/// Wheather the Agent is currently alive.
		private bool _alive = true;
		/// Last time the Agent automatically replenished his energy.
		private float _lastTimeAddedEnergy;
        /// Last time the Agent automatically decreased his intoxication.
        private float _lastTimeReducedIntoxication;
        /// Last time the Agent automatically decreased his morale.
        private float _lastTimeReducedMorale;
        /// Last time the Agent automatically decreased his temperament.
        private float _lastTimeReducedTemperament;

        /// <summary>
        /// Initialization of Agent's Resources component.
        /// </summary>
        /// <param name="agent"></param>
        public void Init(Agent agent)
		{
			_agent = agent;

            //Commented code because I want to set the health to _current value

            //if (_useHealth) _curHealthPoints = _healthPoints;
            //if (_useEnergy) _curEnergyPoints = _energyPoints;
            //         if (_useIntoxication) _curIntoxicationPoints = _intoxicationPoints;
            //         if (_useMorale) _curMoralePoints = _moralePoints;
            //         if (_useTemperament) _curTemperamentPoints = _temperamentPoints

            _agentAnimation = _agent.AgentAnimation;
			Death.Init(agent);
		}

        /// <summary>
        /// Create new resources component with specified configurations.
        /// </summary>
        /// <param name="useHealth"></param>
        /// <param name="maxHealth"></param>
        /// <param name="useEnergy"></param>
        /// <param name="maxEnergy"></param>
        /// <param name="useIntoxication"></param>
        /// <param name="maxIntoxication"></param>
        /// <param name="useMorale"></param>
        /// <param name="maxMorale"></param>
        /// <param name="useTemperament"></param>
        /// <param name="maxTemperament"></param>
        /// <returns></returns>
        public static Resources CreateResources(bool useHealth = false, int maxHealth = 10, bool useEnergy = false,
			int maxEnergy = 10, bool useIntoxication = false, int maxIntoxication = 10, bool useMorale = false, int maxMorale = 10, bool useTemperament = false, int maxTemperament = 10)
		{
			return new Resources
			{
				_useHealth = useHealth,
				_healthPoints = maxHealth,
				_useEnergy = useEnergy,
				_energyPoints = maxEnergy,
                _useIntoxication = useIntoxication,
                _intoxicationPoints = maxIntoxication,
                _useMorale = useMorale,
                _moralePoints = maxMorale,
                _useTemperament = useTemperament,
                _temperamentPoints = maxTemperament,
                Death = new Death()
			};
		}

		/// <summary>
		/// Take Agent's health, locking his components for specified
		/// amount of time and inducingspecific sound and animation.
		/// </summary>
		/// <param name="value"></param>
		public void Damage(int value)
		{
			if (!_useHealth) return;

			_agent.GetAudioSource().PlayRandomClip(_onDamageSounds);
			

			_curHealthPoints = Mathf.Max(0, _curHealthPoints - value);
            if (_curHealthPoints < 0)
            {
                _curHealthPoints = 0;
            }

            if (_lockTime > 0) _agent.Motion.Lock();
			if (_alive && _curHealthPoints <= 0)
			{
				_alive = false;
				Death.Die();
			}

			// Check wheather Agent needs to animate the damage.
			var animate = true;
			try
			{
				animate = !((_agent.CurrentSkill.State == SkillState.Loading
				             || _agent.CurrentSkill.State == SkillState.Invoking)
				            && !_agent.CurrentSkill.Interruptible);
			}
			catch (Exception)
			{
				/*ignore*/
			}

			if (animate)
			{
				switch (_agentAnimation.AnimationMode)
				{
					case AnimationMode.Legacy:
						_agentAnimation.Animate(AnimationState.TakingDamage, 0, true);
						break;
					case AnimationMode.Mecanim:
						_agentAnimation.Animate(_agentAnimation.Parameters.TakeDamageTrigger, _lockTime);
						break;
				}
				
			}
			if (_lockTime > 0) _agent.StartCoroutine(UnlockMovementEnum());
		}

        /// <summary>
        /// Take Agent's intoxication, locking his components for specified
        /// amount of time and inducing specific sound and animation.
        /// </summary>
        /// <param name="value"></param>
        public void Consume(int value)
        {
            if (!_useIntoxication) return;

            //_agent.GetAudioSource().PlayRandomClip(_onConsumeSounds);

            _curIntoxicationPoints = Mathf.Max(0, _curIntoxicationPoints - value);
            if (_lockTime > 0) _agent.Motion.Lock();
            if (_alive && _curIntoxicationPoints >= 10)
            {

                //Debug.Log("DRUNK!!!!  DRUNK!!!   Create animation sequence");
                //_alive = false;
                //Death.Die();
            }

            // Check wheather Agent needs to animate the intoxication.
            var animate = true;
            try
            {
                animate = !((_agent.CurrentSkill.State == SkillState.Loading
                             || _agent.CurrentSkill.State == SkillState.Invoking)
                            && !_agent.CurrentSkill.Interruptible);
            }
            catch (Exception)
            {
                /*ignore*/
            }

            if (animate)
            {
                switch (_agentAnimation.AnimationMode)
                {
                    case AnimationMode.Legacy:
                        //_agentAnimation.Animate(AnimationState.Consume, 0, true);
                        break;
                    case AnimationMode.Mecanim:
                        //_agentAnimation.Animate(_agentAnimation.Parameters.ConsumeTrigger, _lockTime);
                        break;
                }

            }
            if (_lockTime > 0) _agent.StartCoroutine(UnlockMovementEnum());
        }

        /// <summary>
        /// Take Agent's morale, locking his components for specified
        /// amount of time and inducing specific sound and animation.
        /// </summary>
        /// <param name="value"></param>
        public void Pray(int value)
        {
            if (!_useMorale) return;

            //_agent.GetAudioSource().PlayRandomClip(_onPraySounds);

            _curMoralePoints = Mathf.Max(0, _curMoralePoints - value);
            if (_lockTime > 0) _agent.Motion.Lock();
            if (_alive && _curMoralePoints >= 10)
            {

                //Debug.Log("Morale fully restored   Create animation sequence");
                //_alive = false;
                //Death.Die();
            }

            // Check wheather Agent needs to animate the morale.
            var animate = true;
            try
            {
                animate = !((_agent.CurrentSkill.State == SkillState.Loading
                             || _agent.CurrentSkill.State == SkillState.Invoking)
                            && !_agent.CurrentSkill.Interruptible);
            }
            catch (Exception)
            {
                /*ignore*/
            }

            if (animate)
            {
                switch (_agentAnimation.AnimationMode)
                {
                    case AnimationMode.Legacy:
                        //_agentAnimation.Animate(AnimationState.Pray, 0, true);
                        break;
                    case AnimationMode.Mecanim:
                        //_agentAnimation.Animate(_agentAnimation.Parameters.PrayTrigger, _lockTime);
                        break;
                }

            }
            if (_lockTime > 0) _agent.StartCoroutine(UnlockMovementEnum());
        }


        /// <summary>
        /// Take Agent's Temperament, locking his components for specified
        /// amount of time and inducing specific sound and animation.
        /// </summary>
        /// <param name="value"></param>
        public void Temperament(int value)
        {
            if (!_useTemperament) return;

            //_agent.GetAudioSource().PlayRandomClip(_onTemperamentSounds);

            _curTemperamentPoints = Mathf.Max(0, _curTemperamentPoints - value);
            if (_lockTime > 0) _agent.Motion.Lock();
            if (_alive && _curTemperamentPoints >= 10)
            {

                //Debug.Log("Temperament is riot!!!   Create animation sequence");
                //_alive = false;
                //Death.Die();
            }

            // Check wheather Agent needs to animate the morale.
            var animate = true;
            try
            {
                animate = !((_agent.CurrentSkill.State == SkillState.Loading
                             || _agent.CurrentSkill.State == SkillState.Invoking)
                            && !_agent.CurrentSkill.Interruptible);
            }
            catch (Exception)
            {
                /*ignore*/
            }

            if (animate)
            {
                switch (_agentAnimation.AnimationMode)
                {
                    case AnimationMode.Legacy:
                        //_agentAnimation.Animate(AnimationState.Temperament, 0, true);
                        break;
                    case AnimationMode.Mecanim:
                        //_agentAnimation.Animate(_agentAnimation.Parameters.TemperamentTrigger, _lockTime);
                        break;
                }

            }
            if (_lockTime > 0) _agent.StartCoroutine(UnlockMovementEnum());
        }


        /// <summary>
        /// Replenish Agent's health.
        /// </summary>
        /// <param name="value"></param>
        public void Heal(int value)
		{
			if (!_useHealth) return;
			_curHealthPoints = Mathf.Min(_healthPoints, _curHealthPoints + value);
            _curHealthPoints = Mathf.Clamp(_curHealthPoints, 0, 100);
        }

		/// <summary>
		/// Replenish Agent's health for a random value in specified range.
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		public void Heal(int minValue, int maxValue)
		{
			if (!_useHealth) return;
			Heal(UnityEngine.Random.Range(minValue, maxValue));
		}

		/// <summary>
		/// Depending on the value, damage Agent or heal him.
		/// </summary>
		/// <param name="value"></param>
		public void AddHealth(int value)
		{
			if (!_useHealth) return;

			if (value >= 0) Heal(value);
			else Damage(value);
		}

		/// <summary>
		/// Use energy if possible.
		/// </summary>
		/// <param name="value"></param>
		public void UseEnergy(int value)
		{
			if (!_useEnergy) return;
			if (_curEnergyPoints - value >= 0) _curEnergyPoints = Mathf.Max(0, _curEnergyPoints - value);

        }

		/// <summary>
		/// Use random amount of energy in specified range.
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		public void UseEnergy(int minValue, int maxValue)
		{
			if (!_useEnergy) return;
			UseEnergy(UnityEngine.Random.Range(minValue, maxValue));
		}

		/// <summary>
		/// Replenish Agent's energy.
		/// </summary>
		/// <param name="value"></param>
		public void AddEnergy(int value)
		{
			if (!_useEnergy) return;
			_curEnergyPoints = Mathf.Min(_energyPoints, _curEnergyPoints + value);
            _curEnergyPoints = Mathf.Clamp(_curEnergyPoints, 0, 100);
        }

		/// <summary>
		/// Replenish random amount of Agent's energy in specified range.
		/// </summary>
		/// <param name="minValue"></param>
		/// <param name="maxValue"></param>
		public void AddEnergy(int minValue, int maxValue)
		{
			if (!_useEnergy) return;
			AddEnergy(UnityEngine.Random.Range(minValue, maxValue));
		}


        /// <summary>
        /// Use intoxication if possible.
        /// </summary>
        /// <param name="value"></param>
        public void UseIntoxication(int value)
        {
            if (!_useIntoxication) return;
            if (_curIntoxicationPoints - value >= 0) _curIntoxicationPoints = Mathf.Max(0, _curIntoxicationPoints - value);
        }

        /// <summary>
        /// Use random amount of Intoxication in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void UseIntoxication(int minValue, int maxValue)
        {
            if (!_useIntoxication) return;
            UseIntoxication(UnityEngine.Random.Range(minValue, maxValue));
        }

        /// <summary>
        /// Increase Agent's Intoxication.
        /// </summary>
        /// <param name="value"></param>
        public void AddIntoxication(int value)
        {
            if (!_useIntoxication) return;
            _curIntoxicationPoints = Mathf.Min(_intoxicationPoints, _curIntoxicationPoints + value);
            _curIntoxicationPoints = Mathf.Clamp(_curIntoxicationPoints, 0, 100);
        }

        /// <summary>
        /// Increase random amount of Agent's Intoxication in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void AddIntoxication(int minValue, int maxValue)
        {
            if (!_useIntoxication) return;
            AddIntoxication(UnityEngine.Random.Range(minValue, maxValue));
        }

        /// <summary>
        /// Use Morale if possible.
        /// </summary>
        /// <param name="value"></param>
        public void UseMorale(int value)
        {
            if (!_useMorale) return;
            if (_curMoralePoints - value >= 0) _curMoralePoints = Mathf.Min(0, _curMoralePoints - value);
        }

        /// <summary>
        /// Use random amount of Morale in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void UseMorale(int minValue, int maxValue)
        {
            if (!_useMorale) return;
            UseMorale(UnityEngine.Random.Range(minValue, maxValue));
        }

        /// <summary>
        /// Increase Agent's Morale.
        /// </summary>
        /// <param name="value"></param>
        public void AddMorale(int value)
        {
            if (!_useMorale) return;
            _curMoralePoints = Mathf.Min(_moralePoints, _curMoralePoints + value);
            _curMoralePoints = Mathf.Clamp(_curMoralePoints, 0, 100);
        }

        /// <summary>
        /// Increase random amount of Agent's Morale in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void AddMorale(int minValue, int maxValue)
        {
            if (!_useMorale) return;
            AddMorale(UnityEngine.Random.Range(minValue, maxValue));
        }


        /// <summary>
        /// Use Temperament if possible.
        /// </summary>
        /// <param name="value"></param>
        public void UseTemperament(int value)
        {
            if (!_useTemperament) return;
            if (_curTemperamentPoints - value >= 0) _curTemperamentPoints = Mathf.Max(0, _curTemperamentPoints - value);
        }

        /// <summary>
        /// Use random amount of Temperament in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void UseTemperament(int minValue, int maxValue)
        {
            if (!_useTemperament) return;
            UseTemperament(UnityEngine.Random.Range(minValue, maxValue));
        }

        /// <summary>
        /// Increase Agent's Temperament.
        /// </summary>
        /// <param name="value"></param>
        public void AddTemperament(int value)
        {
            if (!_useTemperament) return;
            _curTemperamentPoints = Mathf.Min(_temperamentPoints, _curTemperamentPoints + value);
            _curTemperamentPoints = Mathf.Clamp(_curTemperamentPoints, 0, 100);
        }

        /// <summary>
        /// Increase random amount of Agent's Temperament in specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        public void AddTemperament(int minValue, int maxValue)
        {
            if (!_useTemperament) return;
            AddTemperament(UnityEngine.Random.Range(minValue, maxValue));
        }

        /// <summary>
        /// Update the self-replenishment properties of Agent's Resources.
        /// </summary>
        public void Update()
		{
			if (_useHealth && _healOverTime && Time.time >= _lastTimeHealed + _healCoolDown)
			{
				Heal(_healAmount);
				_lastTimeHealed = Time.time;
			}

			if (_useEnergy && _addEnergyOverTime && Time.time >= _lastTimeAddedEnergy + _addEnergyCoolDown)
			{
				AddEnergy(_addEnergyAmount);
				_lastTimeAddedEnergy = Time.time;
			}

            if (_useIntoxication && _addIntoxicationOverTime && Time.time >= _lastTimeReducedIntoxication + _addIntoxicationCoolDown)
            {
                AddIntoxication(_addIntoxicationAmount);
                _lastTimeReducedIntoxication = Time.time;
            }

            if (_useMorale && _addMoraleOverTime && Time.time >= _lastTimeReducedMorale + _addMoraleCoolDown)
            {
                AddMorale(_addMoraleAmount);
                _lastTimeReducedMorale = Time.time;
            }

            if (_useTemperament && _addTemperamentOverTime && Time.time >= _lastTimeReducedTemperament + _addTemperamentCoolDown)
            {
                AddTemperament(_addTemperamentAmount);
                _lastTimeReducedTemperament = Time.time;
            }
        }

		/// <summary>
		/// Return wheather the health points are at maximum.
		/// </summary>
		/// <returns></returns>
		public bool HealthFull()
		{
			return _curHealthPoints == _healthPoints;
		}

		/// <summary>
		/// Return wheather the energy points are at maximum.
		/// </summary>
		/// <returns></returns>
		public bool EnergyFull()
		{
			return _curEnergyPoints == _energyPoints;
		}

        /// <summary>
        /// Return wheather the intoxication points are at maximum.
        /// </summary>
        /// <returns></returns>
        public bool IntoxicationFull()
        {
            return _curIntoxicationPoints == _intoxicationPoints;
        }


        /// <summary>
        /// Return wheather the morale points are at maximum.
        /// </summary>
        /// <returns></returns>
        public bool MoraleFull()
        {
            return _curMoralePoints == _moralePoints;
        }


        /// <summary>
        /// Return wheather the temperament points are at maximum.
        /// </summary>
        /// <returns></returns>
        public bool TemperamentFull()
        {
            return _curTemperamentPoints == _temperamentPoints;
        }

        /// <summary>
        /// Wait for specified time and unlock Agent's movement.
        /// </summary>
        /// <returns></returns>
        private IEnumerator UnlockMovementEnum()
		{
			yield return new WaitForSeconds(_lockTime);
			_agent.Motion.Unlock();
		}


		#region ADD_INTARFACES
		public ResourcesConditionInterface AddConditionInterface(string methodName)
		{
			return AgentFunctions.AddConditionInterface(methodName, ref _conditionInterfaces, _agent);
		}
		#endregion
    }
}