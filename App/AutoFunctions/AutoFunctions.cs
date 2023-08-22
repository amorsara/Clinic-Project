using Quartz;
using Repository.GeneratedModels;
using Services.Waitings;

namespace App.AutoFunctions
{
    public class AutoFunctions : IJob
    {
        private readonly IWaitingsData _iWaitingsData;


        public AutoFunctions(IWaitingsData iWaitingsData)
        {
            _iWaitingsData = iWaitingsData;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            await _iWaitingsData.DeleteAllWaitingWithPastDate();
        }


    }
}







       

        

