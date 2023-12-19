using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.Jobs
{
    [CreateAssetMenu(fileName = "Job", menuName = "Jobs/Job", order = 1)]
    public class SOJob : ScriptableObject
    {
        [SerializeField] private Sprite jobIcon;
        [SerializeField] private string jobName;
        [SerializeField] private int minimumDefaultSalary;
        [SerializeField] private int maximumDefaultSalary;
        [SerializeField] private int salaryPerLevel;

        public Sprite _JobIcon => jobIcon;

        public string _JobName => jobName;

        public int _MinimumDefaultSalary => minimumDefaultSalary;

        public int _MaximumDefaultSalary => maximumDefaultSalary;

        public int _SalaryPerLevel => salaryPerLevel;
    }
}