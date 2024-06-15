using TestTask;

WorkWithRecords workWithRecords = new WorkWithRecords();
Console.WriteLine("Хотите ли загрузить записи из файла? Да: 1, Нет: 0");
int.TryParse(Console.ReadLine(), out int n);
if (n == 1)
{
    try
    {
        workWithRecords.LoadFromFile(workWithRecords.StoragePath);
    }
    catch(Exception ex) 
    {
        Console.Clear();
        Console.WriteLine(ex.Message);
    }
}

while (true)
{
    Console.WriteLine("------------------------------------------");
    Console.WriteLine("Выберите необходимую операцию из списка: ");
    Console.WriteLine("1: Добавление записи в локальное хранилище");
    Console.WriteLine("2: Удаление записи из локального хранилища");
    Console.WriteLine("3: Вывод записей хранилища");
    Console.WriteLine("4: Изменение записи");
    Console.WriteLine("5: Получить запись при наличии в хранилище");
    Console.WriteLine("0: Остановить выполнение");

    int.TryParse(Console.ReadLine(), out int operation);
    if (operation == 0)
    {
        break;
    }
    switch (operation)
    {
        case 1:
            try
            {
                Console.WriteLine("Ведите название для записи: ");
                string title = Console.ReadLine();
                Console.WriteLine("Введите описание для записи: ");
                string description = Console.ReadLine();
                workWithRecords.AddRecord(title, description);
                
                Console.Clear();
                workWithRecords.PrintRecords();
            }
            catch(Exception ex) 
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                workWithRecords.PrintRecords();
            }
            
            break;

        case 2:
            try
            {
                Console.WriteLine("Введите название записи, которую хотите удалить");
                string toRemove = Console.ReadLine();
                workWithRecords.RemoveRecord(toRemove);

                Console.Clear();
                workWithRecords.PrintRecords();
            }
            catch(Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                workWithRecords.PrintRecords();
            }
            break;

        case 3:
            Console.Clear();
            workWithRecords.PrintRecords();
            break;

        case 4:
            Console.WriteLine("Введите название записи, которую хотите изменить:");
            string toChange = Console.ReadLine();
            Console.WriteLine("Выберите то, что вы хотите изменить:");
            Console.WriteLine("1: Изменить название записи");
            Console.WriteLine("2: Изменить описание записи");
            Console.WriteLine("3: Изменить статус выполнения записи");
            int.TryParse(Console.ReadLine(), out int changeOperation);
            try
            {
                switch (changeOperation)
                {
                    case 1:
                        Console.WriteLine("Введите изменённое название:");
                        string newTitle = Console.ReadLine();
                        workWithRecords.ChangeRecordTitle(toChange, newTitle);
                        break;
                    case 2:
                        Console.WriteLine("Введите изменённое описание: ");
                        string newDescription = Console.ReadLine();
                        workWithRecords.ChangeRecordDescription(toChange, newDescription);
                        break;
                    case 3:
                        Console.WriteLine("Введите изменённый статус 1: Выполнено, 0: В работе");
                        string newStatus = Console.ReadLine();
                        if (newStatus == "0")
                        {
                            workWithRecords.ChangeRecordStatus(toChange, false);
                        }
                        else if (newStatus == "1")
                        {
                            workWithRecords.ChangeRecordStatus(toChange, true);
                        }
                        else
                        {
                            Console.WriteLine("Некорректный статус");
                        }
                        break;
                }
                Console.Clear();
                workWithRecords.PrintRecords();
            }
            catch(Exception ex)
            {
                Console.Clear();
                Console.WriteLine(ex.Message);
                workWithRecords.PrintRecords();
            }
            
            break;
        case 5:
            try
            {
                Console.WriteLine("Введите название записи, которую хотите просмотреть:");
                string checkTitle = Console.ReadLine();
                Console.Clear();
                Record? checkRecord = workWithRecords.ContainsTitle(checkTitle);
                if (checkRecord != null) 
                {
                    Console.WriteLine($"Полученнная запись: {checkRecord}");
                }
                else
                {
                    throw new ArgumentException("Такой записи не существует");
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            break; 
    }

}
