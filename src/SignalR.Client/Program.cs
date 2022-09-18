using Microsoft.AspNetCore.SignalR.Client;
public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter Your UserId :");
        var userId = int.Parse(Console.ReadLine().ToString().Trim());
        ConnectToServer(userId);
        Console.ReadKey();
    }

    public static async void ConnectToServer(int userRef)
    {
        var connection = new HubConnectionBuilder()
             .WithUrl("http://localhost:5232/NotificationHub")
             .Build();

        Console.WriteLine("Connection...");

        await connection.StartAsync();
        connection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await connection.StartAsync();
        };

        connection.On<object>("notifytoast", (message) =>
        {
            Console.WriteLine(message);
        });

        while (true) //todo use events
        {
            if (connection.State == HubConnectionState.Connected)
            {
                Console.WriteLine("Connected to hun successfully!");
                break;
            }
            Thread.Sleep(500);
        }

        await connection.SendAsync("Login", userRef, 1);

        Console.WriteLine($"{userRef} Login to hub!");
        Console.WriteLine("----------------------------");
        Console.WriteLine("Wating for push notification");
    }
}





