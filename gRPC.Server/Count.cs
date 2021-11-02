namespace gRPC.Server
{
    public class Contador
    {
        private int _currentValue;

        public int CurrentValue { get => _currentValue; }

        public void Incrementar()
        {
            _currentValue++;
        }
    }
}
