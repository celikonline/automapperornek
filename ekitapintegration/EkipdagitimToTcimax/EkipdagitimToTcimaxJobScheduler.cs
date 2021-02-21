using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ekitapintegration.EkipdagitimToTcimax.quartz
{
    public class EkipdagitimToTcimaxJobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EkipdagitimToTcimaxJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithSchedule(CronScheduleBuilder.CronSchedule("0/30 * * * * ?"))
                //.WithDailyTimeIntervalSchedule
                //  (s =>
                //     s
                //    .OnEveryDay() //hergün çalışacağı bilgisi
                //    //.StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(13, 30)) //hergün hangi saatte çalışacağı bilgisi
                //    .WithIntervalInSeconds(1000)
                //  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}