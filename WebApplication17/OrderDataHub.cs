using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication17
{
    public class OrderDataHub : Hub
    {
        public async Task Send() //Тестовый метод для проверки работоспособности хаба
        {
            Console.WriteLine("CCCCCCCCCCCCCCCCCCCCCC");
            await this.Clients.All.SendAsync("Receive", "AAAAAAA");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine("PPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
            await base.OnConnectedAsync();
        }
    }
}
