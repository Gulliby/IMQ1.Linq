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

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
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

		[Category("Restriction Operators")]
		[Title("Where - Task 2")]
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

	    [Category("Restriction Operators")]
	    [Title("Where - Task 3")]
	    [Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]

	    public void Linq3()
	    {
	        decimal xMax = 1000;
            dataSource.Customers
                .Where(customer => customer.Orders.Sum(order => order.Total) > xMax).ToList()
                .ForEach(ObjectDumper.Write);
	    }

	    [Category("Restriction Operators")]
	    [Title("Where - Task 4")]
	    [Description("Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]

	    public void Linq4()
	    {
	        decimal xMax = 1000;
	        dataSource.Customers.
	    }
    }
}
