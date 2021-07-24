namespace ReceitaFederal.Helpers
{
    public class StringHelper
    {
        private string _linha;
        private int _posicao;

        public StringHelper(string conteudo) => _linha = conteudo;

        public string Ler(int tamanho)
        {
            var retorno = string.Empty;
            if (_linha.Length > _posicao)
                retorno = _linha.Length > _posicao + tamanho
                    ? _linha.Substring(_posicao, tamanho)
                    : _linha.Substring(_posicao);

            _posicao += tamanho;

            return retorno.Trim();
        }

        public void Adicionar(string conteudo, int tamanho)
        {
            if (conteudo.Length > tamanho)
                _linha += conteudo.Substring(0, tamanho);
            else
                _linha += conteudo + new string(' ', tamanho - conteudo.Length);
        }

        public void Adicionar(long numero, int tamanho)
        {
            if (numero.ToString().Length > tamanho)
                _linha += numero.ToString().Substring(0, tamanho);
            else
                _linha += new string('0', tamanho - numero.ToString().Length) + numero;
        }

        public int Length => _linha.Length;

        public override string ToString() => _linha;
    }
}