using System;
using TcpLibrary;

class ClientApp
{
    static void Main(string[] args)
    {
        Console.Write("Введите фамилию, имя и отчество через запятую: ");
        string fio = Console.ReadLine();

        Console.Write("Участвует в марафоне (1/0): ");
        int marathon = int.Parse(Console.ReadLine());

        Console.Write("Участвует в полумарафоне (1/0): ");
        int halfMarathon = int.Parse(Console.ReadLine());

        Console.Write("Выбран RFID браслет (1/0): ");
        int rfid = int.Parse(Console.ReadLine());

        Console.Write("Выбран нагрудник бегуна (1/0): ");
        int badge = int.Parse(Console.ReadLine());

        Console.Write("Выбрана бутылка для воды (1/0): ");
        int bottle = int.Parse(Console.ReadLine());

        Console.Write("Выбрана бандана с логотипом (1/0): ");
        int bandana = int.Parse(Console.ReadLine());

        Console.Write("Сумма для фонда 'Подари жизнь': ");
        int charity1 = int.Parse(Console.ReadLine());

        Console.Write("Сумма для фонда 'Вера': ");
        int charity2 = int.Parse(Console.ReadLine());

        Console.Write("Сумма для фонда 'Линия жизни': ");
        int charity3 = int.Parse(Console.ReadLine());

        string data = $"{fio},{{{marathon},{halfMarathon}}},{{{rfid},{badge},{bottle},{bandana}}},{{{charity1},{charity2},{charity3}}}";

        TcpClientLib client = new TcpClientLib();
        client.Connect("localhost", 5000);
        client.SendData(data);

        string response = client.ReceiveData();
        Console.WriteLine($"Ответ от сервера: {response}");

        client.Close();
    }
}