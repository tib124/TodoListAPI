using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListAPI.Models
{
    public class TodoList
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime LastUpdate { get; set; } = DateTime.Now;

        public _Status Status { get; set; }

        // Propriedade para representar o status como texto
        [NotMapped]
        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case _Status.OnGoing:
                        return "Em andamento";
                    case _Status.Finished:
                        return "Finalizado";
                    case _Status.NotStarted:
                        return "Não iniciado";
                    default:
                        return "Desconhecido";
                }
            }
        }

        public enum _Status
        {
            OnGoing,
            Finished,
            NotStarted,
        };


    }
}
