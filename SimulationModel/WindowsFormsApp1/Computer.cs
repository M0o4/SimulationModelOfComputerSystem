using System;
using System.Collections.Generic;
using System.Linq;

namespace WindowsFormsApp1
{
	static class Computer
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="queue">очередь</param>
		/// <param name="unloadingMemory">выгрузка из памяти
		/// <para>unloading from memory</para></param>
		/// <param name="memoryForm">формула памяти
		/// <para>memory formula</para></param>
		/// <param name="procSpeed">во сколько шина медленней процессора
		/// <para>how much is the bus slower than the processor</para></param>
		/// <returns></returns>
		public static List<Pipeline> StartConveer(Queue<Job> queue, int unloadingMemory, int memoryForm, int procSpeed)
		{
			var queueMain = new Queue<Job>(queue);
			var listProcess = new List<Pipeline>();
			var cash = false;
			var conveer = false;
			var conveerN = 0;
			var timeConveer = 0;
			var timeCash = 0;
			Job term = null;
			Job termCash = null;
			var queueMainAfterCash = new Queue<Job>();
			var queueCash = new Queue<Job>();
			var try1 = false;

			for (var i = 0; ; i++)
			{
				listProcess.Add(new Pipeline());
				if (!conveer)
				{
					if (queueMainAfterCash.Count != 0)
					{
						term = queueMainAfterCash.Dequeue();
						if (term.Type == OperationType.ControlOperation)
						{
							conveerN = term.TimeDo * procSpeed;
							try1 = true;
						}
						else
						{
							conveerN = term.TimeDo;
						}
						timeConveer = conveerN + unloadingMemory;
						listProcess.Last().Conveer = ConveerParameter.Request;
						listProcess.Last().ConveerN = term.Numb;
						conveer = true;
					}
					else if (queueMain.Count != 0)
					{
						term = queueMain.Dequeue();
						while (term.Cash == MemoryType.NotCache)
						{
							listProcess.Last().ListQuest.Add(term.Numb);
							queueCash.Enqueue(term);
							if (queueMain.Count == 0)
							{
								term = null;
								break;
							}

							term = queueMain.Dequeue();
						}

						if (term != null)
						{
							if (term.Type == OperationType.ControlOperation)
							{
								conveerN = term.TimeDo * procSpeed;
								try1 = true;
							}
							else
							{
								conveerN = term.TimeDo;
							}
							timeConveer = conveerN + unloadingMemory;
							listProcess.Last().Conveer = ConveerParameter.Request;
							listProcess.Last().ConveerN = term.Numb;
							conveer = true;
						}
						else
						{
							listProcess.Last().Conveer = ConveerParameter.NoOperation;
							listProcess.Last().ConveerN = 0;
						}
					}
					else
					{
						listProcess.Last().Conveer = ConveerParameter.NoOperation;
						listProcess.Last().ConveerN = 0;
					}
				}
				else
				{
					if (timeConveer > conveerN)
					{
						listProcess.Last().Conveer = ConveerParameter.Request;
						listProcess.Last().ConveerN = term.Numb;
					}
					else if (timeConveer == conveerN)
					{
						if (term.Type == OperationType.ComputingProcess)
						{
							listProcess.Last().ConveerN = term.Numb;
							listProcess.Last().Conveer = ConveerParameter.ComputingProcess;
						}
						else
						{
							if (try1)
							{
								listProcess.Last().ListQuest.Add(term.Numb);
								try1 = false;
							}
							if (!cash)
							{
								listProcess.Last().ConveerN = term.Numb;
								listProcess.Last().Conveer = ConveerParameter.ControlOperation;
								cash = true;
							}
							else
							{
								listProcess.Last().ConveerN = 0;
								listProcess.Last().Conveer = ConveerParameter.NoOperation;
								timeConveer++;
							}
						}
					}
					else
					{
						listProcess.Last().ConveerN = term.Numb;
						listProcess.Last().Conveer = term.Type == OperationType.ComputingProcess 
							? ConveerParameter.ComputingProcess : ConveerParameter.ControlOperation;
					}
				}
				//выгрузка из памяти
				//unloading from memory
				if (!cash)
				{
					if (queueCash.Count != 0)
					{
						termCash = queueCash.Dequeue();
						cash = true;
						timeCash = memoryForm * procSpeed;
						listProcess.Last().Cash = CashParameter.InCash;
						listProcess.Last().CashN = termCash.Numb;
					}
					else
					{
						listProcess.Last().Cash = CashParameter.OutCash;
						listProcess.Last().CashN = 0;
					}
				}
				else
				{
					if (timeCash != 0)
					{
						listProcess.Last().Cash = CashParameter.InCash;
						if (termCash != null)
							listProcess.Last().CashN = termCash.Numb;
					}
					else
					{
						listProcess.Last().Cash = CashParameter.OutCash;
						listProcess.Last().CashN = 0;
					}
				}

				// формула памяти
				//memory formula
				if (timeConveer != 0)
				{
					if ((--timeConveer) == 0)
					{
						conveer = false;
						if (term != null && term.Type == OperationType.ControlOperation)
						{
							cash = false;
						}
						term = null;
					}
				}
				//во сколько шина медленней процессора
				//how much is the bus slower than the processor
				if (timeCash != 0)
				{
					if ((--timeCash) == 0)
					{
						queueMainAfterCash.Enqueue(termCash);
						cash = false;
						termCash = null;
					}
				}
				if (!conveer && !cash && timeConveer == 0 && timeCash == 0 && queueMain.Count == 0 
					&& queueMainAfterCash.Count == 0 && queueCash.Count == 0)
				{
					return listProcess;
				}
			}
		}

		public static Queue<Job> RandomOperation(int N)
		{
			var rand = new Random();
			var queueMain = new Queue<Job>();
			int rand1;
			int rand2;
			for (var i = 0; i < N; i++)
			{
				var term = new Job();
				rand1 = rand.Next(100);
				term.Cash = (rand.Next(100) < 75) ? MemoryType.Cache : MemoryType.NotCache;
				term.Numb = i + 1;
				rand1 = rand.Next(100);
				rand2 = rand.Next(100);
				if (rand1 < 20)
				{
					term.Type = OperationType.ComputingProcess;
					if (rand2 < 70)
					{
						term.TimeDo = 5;
					}
					else if (rand2 < 90)
					{
						term.TimeDo = 2;
					}
					else
					{
						term.TimeDo = 1;
					}
				}
				else if (rand1 < 35)
				{
					term.Type = OperationType.ComputingProcess;
					if (rand2 < 70)
					{
						term.TimeDo = 2;
					}
					else if (rand2 < 90)
					{
						term.TimeDo = 5;
					}
					else
					{
						term.TimeDo = 1;
					}
				}
				else if (rand1 < 55)
				{
					term.Type = OperationType.ControlOperation;
					term.TimeDo = rand2 < 80 ? 2 : 1;
				}
				else
				{
					term.Type = OperationType.ComputingProcess;
					term.TimeDo = rand2 < 60 ? 2 : 1;
				}
				queueMain.Enqueue(term);
			}
			return queueMain;
		}
	}
}
