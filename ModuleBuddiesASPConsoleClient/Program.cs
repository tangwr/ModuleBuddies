using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Hubs;


namespace ModuleBuddiesASPConsoleClient
{
    class Program
    {
        static void Main(string[] arguments)
        {


            var connection = new HubConnection("http://modulebuddies.azurewebsites.net/TestDraw"); // Since we are client, we must specify a URL
            var shapeHub = connection.CreateHubProxy("shape"); //or Kh2ndcore.CollaborativeWhiteBoard.ShapeHub

            // Register thge events that we are intereted in - similar to the JS client creating methods to be called back upon by the server
            shapeHub.On<string, int, int, int, int, string>("lineDrawn", (cid, fromX, fromY, toX, toY, color) =>
            {
                Console.WriteLine("lineDrawn => Connection Id: {0}, FromX:{1}, FromY: {2}, ToX:{3}, ToY: {4}, Color: {5}", cid, fromX, fromY, toX, toY, color);
            });

            shapeHub.On<string, int, int, string, string>("textTyped", (cid, x, y, text, color) =>
            {
                Console.WriteLine("textTyped => Connection Id: {0}, x:{1}, y: {2}, text:{3}, Color: {4}", cid, x, y, text, color);
            });

            shapeHub.On("canvasCleared", () => Console.WriteLine("Canvas Cleared!!!"));

            shapeHub.On<int>("connectionsCountUpdate", (c) =>
            {
                Console.WriteLine("connectionsCountUpdate => Count: {0}", c);
            });

            shapeHub.On<List<string>>("usersUpdated", (users) =>
            {
                foreach (string user in users)
                    Console.WriteLine("usersUpdated => User: {0}", user);
            });

            // Make the connection
            connection.Start().Wait();

            // Register the console application with a black color
            shapeHub.Invoke("registerUser", connection.ConnectionId, "console", "#000000").Wait();

            int increment = 10;
            int fromXPoint = 0;
            int fromYPoint = 0;
            int toXPoint = increment;
            int toYPoint = 0;

            shapeHub.Invoke("drawLine", connection.ConnectionId, toXPoint, toYPoint, fromXPoint, fromYPoint, "#000000");

            for (int i = 0; i < 10; i++)
            {
                fromYPoint += increment;
                toXPoint += increment;
                toYPoint += increment;
                shapeHub.Invoke("drawLine", connection.ConnectionId, toXPoint, toYPoint, fromXPoint, fromYPoint, "#000000");
                Thread.Sleep(1000);
            }

            fromYPoint += (increment * 2);
            shapeHub.Invoke("typeText", connection.ConnectionId, fromXPoint, fromYPoint, "Hello from the console app!", "#000000");

            connection.Stop();
            Console.Read();
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
