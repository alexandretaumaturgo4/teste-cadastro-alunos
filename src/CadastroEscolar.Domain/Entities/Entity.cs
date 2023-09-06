namespace CadastroEscolar.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime CriadoEm { get; private set; }
    public string Nome { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        CriadoEm = DateTime.Now;
    }
}