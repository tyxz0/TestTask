using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask;

public class Record
{
    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime TimeOfCreation;

    public DateTime? TimeOfChange { get; set; }
    public bool IsDone { get; set; }

    public Record(string title, string description, DateTime timeOfCreation, bool isDone)
    {
        Title = title;
        Description = description;
        TimeOfCreation = timeOfCreation;
        IsDone = isDone;
    }

    public Record(string title, string description, DateTime timeOfCreation, DateTime timeOfChange, bool isDone)
    {
        Title = title;
        Description = description;
        TimeOfCreation = timeOfCreation;
        TimeOfChange = timeOfChange;
        IsDone = isDone;
    }

    public override string ToString()
    {
        if (TimeOfChange != null)
        {
            return $"{Title}|{Description}|{TimeOfCreation.ToString()}|{TimeOfChange.ToString()}|{IsDone.ToString()}";
        }
        return $"{Title}|{Description}|{TimeOfCreation.ToString()}|не было изменено|{IsDone.ToString()}";
    }

}
