using fit5032.order_sender.Models;
using fit5032.order_sender.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace fit5032.order_sender.Controllers
{
    public class OrderController : Controller
    {
        private readonly IServiceBusSenderService _serviceBusSenderService;

        public OrderController(IServiceBusSenderService serviceBusSenderService)
        {
            _serviceBusSenderService = serviceBusSenderService;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            return View();
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderViewModel order, CancellationToken cancellationToken = default)
        {
            try
            {
                OrderMessage orderMessage = new OrderMessage
                {
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Email = order.Email,
                    ProductName = order.ProductName,
                    Quantity = order.Quantity,
                    Status = "Created"
                };

                await _serviceBusSenderService.SendMessageAsync(orderMessage, cancellationToken);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
