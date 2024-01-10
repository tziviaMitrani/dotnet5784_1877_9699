//namespace BlImplementation;
//using BlApi;
//using BlTest.BO;
//using System;
//using System.Collections.Generic;


////internal class TaskImplementation : ITask
////{
////    private DalApi.IDal _dal = Factory.Get;
////    public int Create(BO.Task boTask)
////    {
////        try
////        {

////            if (boTask.Id > 0 && boTask.Description != "")
////            {
////                DO.Task doTask = new DO.Task
////                (boTask.Id, boTask.Description, boTask.Alias, false, boTask.CreatedAtDate, null, boTask.StartDate, boTask.ScheduledDate, boTask.DeadlineDate,
////                boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, null, (DO.EngineerExperience)boTask.Copmlexity);

////            }
////            else {
////                throw new BO.BlNullPropertyException($"Engineer with ID={boTask.Id} already exists", ex);
////            }

////        }
////        catch (Exception ex)
////        {

////            Console.WriteLine(ex);
////        }
////        /* boTask.Dependencies,  boTask.StartDate, 
////          boTask.ForecastDate,  boTask.Deliverables, boTask.Remarks, boTask.Engineer, (DO.EngineerExperience )boTask.Copmlexity);*/

////    }
////}

//    /*
//    //BO public int Id { get; init; }
//    //public required string Description { get; set; }
//    //public required string Alias { get; set; }
//    //public DateTime? CreatedAtDate { get; set; }
//    //public Status status { get; set; }
//    //public required List<TaskInList> Dependencies { get; set; }
//    //public required MilestoneInTask Milestone { get; set; }
//    //public required DateTime? StartDate { get; set; }
//    //public DateTime? ScheduledDate { get; set; }
//    //public DateTime? ForecastDate { get; set; }
//    //public DateTime? DeadlineDate { get; set; }
//    //public DateTime? CompleteDate { get; set; }
//    //public DateTime? Deliverables { get; set; }
//    //public required string Remarks { get; set; }
//    //public required EngineerInTask Engineer { get; set; }
//    //public EngineerExperience Copmlexity { get; set; }


//    //DO int Id,
//    //    string Description,
//    //    string? Alias,
//    //    bool Milestone,
//    //    DateTime? CreatedAtDate,//CreatedAtDate<-ProductionDate
//    //    TimeSpan? RequiredEffortTime,
//    //    DateTime? StartDate,
//    //    DateTime? ScheduledDate,//ScheduledDate<-EstimatedCompletionDate
//    //    DateTime? DeadlineDate,//DeadlineDate<-FinalDateCompletion
//    //    DateTime? CompleteDate,//CompleteDate<-ActualEndDate
//    //    string? Deliverables,//product,
//    //    string? Remarks,//Notes,
//    //    int? Engineerid,
//    //    int Difficulty

//    public void Delete(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public BO.Task? Read(int id)
//    {
//        throw new NotImplementedException();
//    }

//    public IEnumerable<BO.Task> ReadAll(Func<BO.Engineer?, bool>? filter)
//    {
//        throw new NotImplementedException();
//    }



//    public void Update(BO.Task item)
//    {
//        throw new NotImplementedException();
//    }
//}
