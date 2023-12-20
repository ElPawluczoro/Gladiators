using System;
using System.Collections;
using System.Collections.Generic;
using GameScripts.Core;
using GameScripts.Gladiators;
using UnityEngine;
using Random = System.Random;

namespace GameScripts.Jobs
{
    public class JobsController : MonoBehaviour
    {
        [SerializeField] private List<SOJob> jobsAvailable;
        private static Dictionary<SOJob, Gladiator> jobs = new ();
        private Random random;

        private PlayerGladiators playerGladiators;

        private static SOJob _farmJob; 
        [SerializeField] private SOJob farmJob; 
        
        private void Start()
        {
            playerGladiators = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGladiators>();
            _farmJob = farmJob;
            random = new Random();
            
            ToursController.onTourEnd += OnTourEnd;
            
            foreach (SOJob job in jobsAvailable)
            {
                jobs.Add(job, null);
            }
        }

        public static Gladiator GetFarmer()
        {
            return jobs[_farmJob];
        }

        public void AssignGladiator(SOJob job, Gladiator gladiator)
        {
            jobs[job] = gladiator;
        }

        public void OnTourEnd()
        {
            AddFarmerStacks();
            UpdateGladiatorsJobs();
            GetSalaryFromJobs();
            ResetGladiatorAssignation();
        }
        
        private void ResetGladiatorAssignation()
        {
            foreach (SOJob job in jobsAvailable)
            {
                jobs[job] = null;
            }
        }

        private void GetSalaryFromJobs()
        {
            foreach (var job in jobs)
            {
                if (job.Value != null)
                {
                    CoinsController.AddCoins(
                        random.Next(job.Key._MinimumDefaultSalary, job.Key._MaximumDefaultSalary) +
                        job.Key._SalaryPerLevel * job.Value.gladiatorLevel + job.Value.bonusJobsGold[job.Key.Job]
                    );
                }
            }
        }

        private void AddFarmerStacks()
        {
            if (jobs[farmJob] == null) return;
            jobs[farmJob].AddFarmerStacks();
        }

        public Gladiator GetCurrentlyChosenGladiator(SOJob job)
        {
            if (jobs[job] != null)
            {
                return jobs[job];
            }

            return null;
        }

        public List<Gladiator> GetAssignedGladiators()
        {
            List<Gladiator> gladiators = new();
            foreach (var job in jobs)
            {
                if (job.Value != null)
                {
                    gladiators.Add(job.Value);
                }
            }

            return gladiators;
        }

        public void SetGladiatorsLastFarmer()
        {
            if(jobs[farmJob] == null) return;
            jobs[farmJob].isLastJobFarmer = true;
        }

        public void UpdateGladiatorsJobs()
        {
            foreach (GameObject gladiator in playerGladiators.playerGladiatorsList)
            {
                gladiator.GetComponent<Gladiator>().isLastJobFarmer = false;
            }

            SetGladiatorsLastFarmer();
        }
        
    }   
}
