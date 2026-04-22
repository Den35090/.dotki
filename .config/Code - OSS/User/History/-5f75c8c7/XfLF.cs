using System;
using System.Net.NetworkInformation;

class Program
{
    static void Main()
    {
        Ping ping = new Ping();

        Console.Write("Введите адрес: ");
        string address = Console.ReadLine();

        var reply = ping.Send(address);

        Console.WriteLine("Статус: " + reply.Status);
        Console.WriteLine("Время: " + reply.RoundtripTime + " ms");
    }
}