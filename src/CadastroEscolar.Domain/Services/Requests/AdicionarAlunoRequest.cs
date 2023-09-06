namespace CadastroEscolar.Domain.Services.Requests;

public class AdicionarAlunoRequest
{
    public Guid ProfessorId { get; set; }
    public string Nome { get; set; }
    public decimal Mensalidade { get; set; }
    public DateTime DataVencimento { get; set; }
}