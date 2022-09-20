using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQSnippets
{
	public class Snippets
	{
		static public void BasicLinQ()
		{
			string[] cars = {
				"VW Golf",
				"VW California",
				"Audi A3",
				"Audi A5",
				"Fiat Punto",
				"Seat Ibiza",
				"Seat León"
			};

			//SELECT * of Cars
			var carList = from car in cars select car;

			foreach (var car in carList)
			{
				Console.WriteLine(car);
			}

			//SELECT where car is Audi
			var audiList = from car in cars where car.Contains("Audi") select car;
			foreach (var car in audiList)
			{
				Console.WriteLine(car);
			}
		}
		//Number Examples
		static public void LinqNumbers()
		{
			List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

			//Each number * 3
			//Take all numbers but 9
			//Sort ascending

			var processedNumberList =
				numbers
					.Select(num => num * 3)
					.Where(num => num != 9)
					.OrderBy(num => num);
			foreach (var number in processedNumberList)
			{
				Console.Write(number + ", ");
			}
		}
		static public void SearchExamples()
		{
			List<string> textList = new List<string>
			{
				"a",
				"bx",
				"c",
				"d",
				"e",
				"cj",
				"f",
				"c"
			};
			//Find first element
			var first = textList.First();
			//Fist element that is "C"
			var cText = textList.First(tx => tx.Equals("c"));
			//first element that contains "J"
			var jText = textList.First(tx => tx.Contains("j"));
			//first element that contains "j" or present default value
			var jOrDefaultText = textList.FirstOrDefault(tx => tx.Contains("j"));
			//last element that contains "tz" or present default value
			var zOrDefaultText = textList.LastOrDefault(tx => tx.Contains("z"));
			//UNique value
			var uniqueValueText = textList.Single();
			var uniqueOrDefaultText = textList.SingleOrDefault();

			int[] evenNumbers = { 0, 2, 4, 6, 8 };
			int[] otherEvenNumbers = { 0, 2, 6 };

			//Obtein 4 and 8
			var myEvenNumbers = evenNumbers.Except(otherEvenNumbers);
		}
		static public void MultipleSelects()
		{
			//Select many
			string[] myOpinions =
				{
					"opinion 1, text 1",
					"opinion 2, text 2",
					"opinion 3, text 3"

				};
			var opinionSelection = myOpinions.SelectMany(tx => tx.Split(", "));

			var enterprises = new[]
			{
				new Enterprise()
				{
					Id = 1,
					Name = "Enterprise 1",
					Employees = new[]
					{
						new Employee()
						{
							Id = 1,
							Name = "Pepe",
							Email = "pepe@prueba.com",
							Salary = 3000
						},
						new Employee()
						{
							Id = 2,
							Name = "Juan",
							Email = "juan@prueba.com",
							Salary = 2000
						},
						new Employee()
						{
							Id = 3,
							Name = "Mario",
							Email = "mario@prueba.com",
							Salary = 1500
						},
						new Employee()
						{
							Id = 4,
							Name = "Jose",
							Email = "jose@prueba.com",
							Salary = 500
						}
					}
				},
				new Enterprise()
				{
					Id = 2,
					Name = "Enterprise 2",
					Employees = new[]
					{
						new Employee()
						{
							Id = 5,
							Name = "Martin",
							Email = "martin@prueba.com",
							Salary = 3000
						},
						new Employee()
						{
							Id = 6,
							Name = "Ana",
							Email = "ana@prueba.com",
							Salary = 2000
						},
						new Employee()
						{
							Id = 7,
							Name = "Rodolfo",
							Email = "rodolfo@prueba.com",
							Salary = 1500
						},
						new Employee()
						{
							Id = 8,
							Name = "Marta",
							Email = "marta@prueba.com",
							Salary = 500
						}
					}
				}
			};
			//Obtener todos los objetos (Empleados) de todas las empresas

			var employeeList = enterprises.SelectMany(enterprise => enterprise.Employees);

			//Saber si cualquier lista está vacía

			bool hasEnterprises = enterprises.Any();
			bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

			//Todas las empresas con empleados con al menos 1000€ de salario
			bool hasEnterprisesWithEmployeesWithAtLeastOrEqual1000Salary =
				enterprises.Any(enterprise => enterprise.Employees
				.Where(employee => employee.Salary >= 1000).Any());

			
		}

		static public void LinqCollections()
		{
			var firstList = new List<string> { "a", "b", "c"};
			var secondList = new List<string> { "a", "c", "d"};

			//INNER JOIN
			var commonResult = from element in firstList
							   join secondElement in secondList
							   on element equals secondElement
							   select new {element, secondElement};

			var commonResult2 = firstList.Join(secondList
								, element => element
								, secondElement => secondElement
								, (element, secondElement) => new { element, secondElement}
								);

			//OUTER JOIN - LEFT
			var leftOuterJoin = from element in firstList
								join secondElement in secondList
								on element equals secondElement
								into temporalList
								from temporalElement in temporalList.DefaultIfEmpty()
								where element != temporalElement
								select new { Element = element };
			var leftOuterJoin2 = from element in firstList
								 from secondElement in secondList.Where(s => s == element).DefaultIfEmpty()
								 select new { Element = element, SecondElement = secondElement };


			var rightOuterJoin = from secondElement in secondList
								join element in firstList
								on secondElement equals element
								into temporalList
								from temporalElement in temporalList.DefaultIfEmpty()
								where secondElement != temporalElement
								select new { Element = secondElement };
			//UNION
			var unionList = leftOuterJoin.Union(rightOuterJoin);
		}
		static public void SkipTakeLinq()
		{
			var myList = new[]
			{
				1,2,3,4,5,6,7,8,9
			};
			//SKIP
			var skipFirstTwoValues = myList.Skip(2); //{3,4,5,6,7,8,9}
			var skipLastTwoValues = myList.SkipLast(2); //{1,2,3,4,5,6,7}
			var skipWhileSmallerThanFour = myList.SkipWhile(num => num < 4); //{4,5,6,7,8,9}
			//TAKE
			var takeFirstTwoValues = myList.Take(2); //{1,2}
			//lo demás igual que skip

		}

		
		

	}
}
