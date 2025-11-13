using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ApiEcommerce.Hubs;

public class EstadisticasHub : Hub
{
	public override Task OnConnectedAsync()
		{
				Console.WriteLine($"Cliente conectado: {Context.ConnectionId}");
				return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception? exception)
		{
				Console.WriteLine($"Cliente desconectado: {Context.ConnectionId}");
				return base.OnDisconnectedAsync(exception);
		}
    
}
