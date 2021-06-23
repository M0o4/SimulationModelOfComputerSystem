using System.Collections.Generic;

namespace WindowsFormsApp1
{
    public enum CashParameter
    {
        InCash, //есть элемент в кэше | there is an item in the cache
        OutCash //нет элемента в кэше | no item in the cache
    }

    public enum ConveerParameter
    {
        Request, //запрос
        ComputingProcess, // вычислительный процесс
        ControlOperation, // перация управления
        NoOperation // нет операции
    }
    
    public class Pipeline
    {
        public Pipeline()
        {
            ListQuest = new List<int>();
        }
        // номер элемента в кэше
        // item number in the cache
        public int CashN { get; set; }
        
        public CashParameter Cash;
        // номер элемента в конвейере
        // item number in the pipeline
        public int ConveerN { get; set; }
        
        public ConveerParameter Conveer { get; set; }

        // 0 - если нет запроса \\другие номер запроса
        // 0 - if there is no request \ \ other request number
        public List<int> ListQuest { get; set; }
    }
}
