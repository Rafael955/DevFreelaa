namespace DevFreela.Core.Services
{
    public interface IMessageBusService
    {

        /// <summary>
        ///  Método para publicar a mensagem
        /// </summary>
        /// <param name="queue">Fila para qual irá se publicar a mensagem</param>
        /// <param name="message">Mensagem convertida em array de bytes a ser publicada</param>
        void Publish(string queue, byte[] message);
    }
}
