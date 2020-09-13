using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        private void Update()
        {
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

        private static void AttackBehaviour()
        {
            GetComponent<Animator>().SetTrigger("attack");
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

        // Animation Event Called from animator controller
        void Hit()
        {

        }
    }
    
}
