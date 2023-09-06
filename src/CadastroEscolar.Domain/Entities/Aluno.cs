namespace CadastroEscolar.Domain.Entities;

public class Aluno : Entity
{
    public decimal Mensalidade { get; private set; }
    public DateTime DataVencimento { get; private set; }
    public Guid ProfessorId { get; private set; }
    public Professor Professor { get; set; }

    public Aluno(string nome, decimal mensalidade, DateTime dataVencimento, Guid professorId)
    {
        Nome = nome;
        Mensalidade = mensalidade;
        DataVencimento = dataVencimento;
        ProfessorId = professorId;
    }

    protected Aluno()
    {
    }
}