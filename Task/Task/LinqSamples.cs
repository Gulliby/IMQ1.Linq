// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("1 - 3")]
		[Title("Task 1")]
		[Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. " +
		             "Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)")]
		public void Linq1()
		{
			int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
		    var xlist = new List<int> { 5, 7 };

		    xlist.ForEach(x =>
		        {
                    Console.WriteLine($@"Number < {x}");
		            numbers
		                .Where(element => element > x)
		                .ToList()
		                .ForEach(element => Console.WriteLine("{0}", element));
		        }
		    );
		}

		[Category("1 - 3")]
		[Title("Task 2")]
		[Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе")]

		public void Linq2()
		{
            dataSource.Customers.ForEach(customer =>
            {
                Console.WriteLine($@"Customer city: {customer.City};  Customer country: {customer.Country}");
                dataSource.Suppliers
                .Where(supplier => supplier.City.Equals(customer.City) && supplier.Country.Equals(customer.Country)).ToList()
                .ForEach(ObjectDumper.Write);
            });
        }

	    [Category("1 - 3")]
	    [Title("Task 3")]
	    [Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]

	    public void Linq3()
	    {
	        decimal xMax = 1000;
            dataSource.Customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > xMax).ToList()
                .ForEach(ObjectDumper.Write);
	    }

	    [Category("4 - 6")]
	    [Title("Task 4")]
	    [Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]

	    public void Linq4()
	    {
            dataSource
                .Customers
                .Where(customer => customer.Orders.Length >= 1).ToList()
                .ForEach(customer => Console.WriteLine($"{customer.CustomerID} {customer.Phone} {customer.Address} {customer.Orders.Min(order => order.OrderDate).ToShortDateString()}"));
	    }

        [Category("4 - 6")]
        [Title("Task 5")]
        [Description("Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента")]

        public void Linq5()
        {
            dataSource.Customers.Select(c => new
            {
                Customer = c.CompanyName,
                Date = c.Orders.Any() ?
                    c.Orders.DefaultIfEmpty().OrderBy(o => o.OrderDate).FirstOrDefault().OrderDate :
                    new DateTime()
            })
            .OrderBy(c => c.Date.Year)
            .ThenBy(c => c.Date.Month)
            .ThenBy(c => c.Customer).ToList()
            .ForEach(customer => ObjectDumper.Write(customer.Customer + ":  " + customer.Date.ToShortDateString()));
        }

        [Category("4 - 6")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»)")]

        public void Linq6()
        {
            var letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var operatorCodeSymbols = new char[] { '(', ')' };
            dataSource
                .Customers
                .Where(customer => (!string.IsNullOrEmpty(customer.PostalCode) && customer.PostalCode.IndexOfAny(letters) > -1)
                    || string.IsNullOrEmpty(customer.Region)
                    || customer.Phone.IndexOfAny(operatorCodeSymbols) == -1).Select(customer => customer.CompanyName).ToList()
                    .ForEach(customer => ObjectDumper.Write(customer));
        }

        [Category("7 - 10")]
        [Title("Task 7")]
        [Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости")]

        public void Linq7()
        {
            dataSource
                .Products
                .OrderBy(p => p.Category)
                .ThenBy(p => p.UnitsInStock)
                .ThenBy(p => p.UnitPrice).ToList()
                .ForEach(product => ObjectDumper.Write(product.Category + "  " + product.UnitsInStock + "  " + product.UnitPrice));
        }

        [Category("7 - 10")]
        [Title("Task 8")]
        [Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами")]

        public void Linq8()
        {
            var smaillPrice = 3;
            var mediumPrice = 12;

            var products1 = dataSource.Products.Where(p => p.UnitPrice <= smaillPrice);
            var products2 = dataSource.Products.Where(p => p.UnitPrice <= mediumPrice && p.UnitPrice >= smaillPrice);
            var products3 = dataSource.Products.Where(p => p.UnitPrice >= mediumPrice);

            products1
                .Concat(products2)
                .Concat(products3).ToList()
                .ForEach(customer => ObjectDumper.Write(customer.ProductName + "  " + customer.UnitPrice));

        }

        [Category("7 - 10")]
        [Title("Task 9")]
        [Description("Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)")]
        public void Linq9()
        {
            dataSource.Customers
                .GroupBy(x => x.City,
                    (city, customers) =>
                    {
                        var totalOrders = customers.Sum(y => y.Orders.Length);

                        return new
                        {
                            City = city,
                            Profitability = customers.Sum(y => y.Orders.Sum(z => z.Total)) / totalOrders,
                            Intensity = totalOrders / customers.Count()
                        };
                    }).ToList()
                    .ForEach(cityInfo => Console.WriteLine(cityInfo.City + "    " + cityInfo.Profitability + "    " + cityInfo.Intensity));
        }

        [Category("7 - 10")]
        [Title("Task 10")]
        [Description("Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void Linq10()
        {
            dataSource.Customers.SelectMany(x => x.Orders, (customer, order) => new
            {
                Month = order.OrderDate.Month,
                Order = order
            }).GroupBy(x => x.Month, (month, orders) => new
            {
                Month = month,
                Amount = orders.Count()
            }).OrderBy(x => x.Month).ToList()
            .ForEach(info => Console.WriteLine(info.Month + "   " + info.Amount));
        }
    }
}
