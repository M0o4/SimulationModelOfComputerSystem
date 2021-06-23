
namespace WindowsFormsApp1
{
    public enum OperationType
    {
        ComputingProcess,//вычислительный процесс
        ControlOperation //операция управления
    }

    public enum MemoryType
    {
        Cache, //в кэш
        NotCache //не в кэш
    }
    
    public class Job
    {
        public int Numb { get; set; }
        public int TimeDo { get; set; }
        
        public OperationType Type { get; set; }
        
        public MemoryType Cash { get; set; }

        public Job() { }
        public Job(int timeDo, OperationType type, MemoryType cash)
        {
            TimeDo = timeDo;
            Type = type;
            Cash = cash;
        }
    }
}
