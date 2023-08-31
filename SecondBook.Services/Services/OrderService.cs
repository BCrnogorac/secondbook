﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SecondBook.EF.Database;
using SecondBook.EF.Database.Models;
using SecondBook.Services.Models.BM;
using SecondBook.Services.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBook.Services.Services
{
    public class OrderService : BaseService
    {
        public OrderService(SecondBookDBContext dbContext, IMapper mapperConfiguration) : base(dbContext, mapperConfiguration)
        {
        }

        public void InsertOrder(OrderBM model)
        {
            var books = dbContext.Books.Where(x => model.BookIds.Contains(x.Id)).ToList();

            if (model == null || books.Count == 0)
            {
                return;
            }
            decimal totalPrice = books.Sum(book => book.Price);

            var orderModel = new Order()
            {
                UserId = model.UserId,
                Books = books,
                CreatedDate = DateTime.UtcNow,
                Price = totalPrice
            };
            dbContext.Orders.Add(orderModel);
            dbContext.SaveChanges();
        }

        public IEnumerable<OrderDTO> GetOrdersByUserId(int id)
        {
            var query = dbContext.Orders.Include(x => x.Books).Where(x => x.UserId == id).ToList();
            return mapper.Map<IEnumerable<OrderDTO>>(query);
        }

        public void DeleteOrderById(int id)
        {
            var order = dbContext.Orders.Find(id);
            if (order != null)
            {
                dbContext.Orders.Remove(order);
                dbContext.SaveChanges();
            }
        }
    }
}
