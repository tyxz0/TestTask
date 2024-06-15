using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask;

public class WorkWithRecords
{
    private List<Record> records_;

    //папка appdata, поменять если нужно
    public string StoragePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Records.txt");
        
    public WorkWithRecords()
    {
        records_ = new List<Record>();
    }

    public void AddRecord(string title, string description)
    {
        if (ContainsTitle(title) != null )
        {
            throw new ArgumentException("Такая запись уже существует");
        }
        records_.Add(new Record(title, description, DateTime.Now, false));
        SaveToFile(StoragePath);
    }

    public void RemoveRecord(string title)
    {
        Record? recordToRemove = ContainsTitle(title) ?? throw new ArgumentException("Такой записи не существует");
        records_.Remove(recordToRemove);
        Console.WriteLine("Запись успешно удалена");
        SaveToFile(StoragePath);

    }
    public void PrintRecords()
    {
        Console.WriteLine("Текущие доступные записи:");
        foreach (Record record in records_) 
        {
            Console.WriteLine(record.ToString());
        }
    }

    public Record? ContainsTitle(string title)
    {
        foreach(Record record in records_)
        {
            if (record.Title == title)
            {
                return record;
            }
        }
        return null;
    }

    public void ChangeRecordTitle(string title, string newTitle)
    {
        if (ContainsTitle(newTitle) != null)
        {
            throw new ArgumentException("Запись с таким названием уже существует");
        }
        Record? recordToChange = ContainsTitle(title);
        if (recordToChange != null && ContainsTitle(newTitle) == null)
        {
            recordToChange.Title = newTitle;
            recordToChange.TimeOfChange = DateTime.Now;
            SaveToFile(StoragePath);

        }
        else
        {
            throw new ArgumentException("Такой записи не существует");
        }
    }
    public void ChangeRecordDescription(string title, string newDescription)
    {
        Record? recordToChange = ContainsTitle(title);
        if (recordToChange != null)
        {
            recordToChange.Description = newDescription;
            recordToChange.TimeOfChange = DateTime.Now;
            SaveToFile(StoragePath);
        }
        else
        {
            throw new ArgumentException("Такой записи не существует");
        }
    }

    public void ChangeRecordStatus(string title, bool isDoneUpdate)
    {
        Record? recordToChange = ContainsTitle(title);
        if (recordToChange != null)
        {
            recordToChange.IsDone = isDoneUpdate;
            recordToChange.TimeOfChange = DateTime.Now;
            SaveToFile(StoragePath);
        }
        else
        {
            throw new ArgumentException("Такой записи не существует");
        }
    }

    public void SaveToFile(string filename) 
    { 
        if (records_.Count == 0)
        {
            throw new Exception("Отсутствуют записи для сохранения");
        }

        if (File.Exists(filename))
        {
            File.Delete(filename);
        }

        using StreamWriter sw = new(filename);
        {
            foreach (Record record in records_)
            {
                sw.WriteLine(record.ToString());
            }
        }
    }

    public void LoadFromFile(string filename) 
    { 
        if (!File.Exists(filename))
        {
            throw new Exception("Файла для загрузки не существует");
        }

        string? bufferLine = "";

        using StreamReader sr = new(filename);
        {
            while ((bufferLine = sr.ReadLine()) != null)
            {
                string[] record = bufferLine.Split("|", StringSplitOptions.RemoveEmptyEntries);
                if (record.Length == 5)
                {
                    if (record[3] == "не было изменено")
                    {
                        records_.Add(new Record(record[0], record[1], Convert.ToDateTime(record[2]), Convert.ToBoolean(record[4])));
                    }
                    else
                    {
                        records_.Add(new Record(record[0], record[1], Convert.ToDateTime(record[2]), Convert.ToDateTime(record[3]), Convert.ToBoolean(record[4])));
                    }
                
                }
            }
        }

    }
}
