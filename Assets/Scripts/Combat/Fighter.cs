using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            // Dont do anything if I haven't clicked on an enemy
            if(target == null) return;

            // Check distance from player to enemy
            // If its not in range: move to the enemy
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            // If it is in range: 1. stop moving | 2. Attack
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();

            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger Hit() animation event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                
            }
        }

        // Animation Event Called from animator controller
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        
    }
    
}
