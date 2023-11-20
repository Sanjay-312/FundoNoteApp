using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Bussiness_Layer.Interfaces;

namespace FundooNote.Controllers
{
    public class TicketController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IUserBussiness userBussiness;
        public TicketController(IBus bus,IUserBussiness userBussiness)
        {
            _bus = bus;
            userBussiness = userBussiness;
        }
        [HttpPost]

        public async Task<IActionResult> CreateTicket(string email)
        {
            if (email != null)
            {
                var token=userBussiness.ForgetPassword(email);
                if(!string.IsNullOrEmpty(token))
                {
                    var ticketResponse=userBussiness.CreateTicketForPassword(email,token);
                    Uri uri = new Uri("rabbitmq://localhost/ticketQueue");
                    var endPoint = await _bus.GetSendEndpoint(uri);
                    await endPoint.Send(ticketResponse);
                    return Ok(new {status=true,});
                }
                else
                {
                    return BadRequest();
                }
                
               
            }
            return BadRequest();
        }
    }
}
