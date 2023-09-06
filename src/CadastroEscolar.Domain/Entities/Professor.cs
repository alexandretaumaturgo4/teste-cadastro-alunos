namespace CadastroEscolar.Domain.Entities;

public class Professor : Entity
{
    public bool Bloqueado
    {
        get
        {
            var isBlocked = BloqueadoAte >= DateTime.Now;
            return isBlocked;
        } 
    }

    public DateTime? BloqueadoAte { get; private set; }

    public Professor(string nome)
    {
        Nome = nome;
    }
    
    protected Professor(){}

    public void Bloquear(int seconds)
    {
        BloqueadoAte = DateTime.Now.AddSeconds(seconds);
    }
}