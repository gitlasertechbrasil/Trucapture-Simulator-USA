using System;

namespace TruCaptureEmulator
{
    public class ConfiguracaoImagem
    {
        private TipoContraste _Contraste;
        private TipoShutter _Shutter;
        private int _ValorShutter;
        private int _ShutterMaximo;
        private TipoGanho _TipoGanho;
        private TipoGanhoEstendido _TipoGanhoEstendido;
        private int _ValorGanho;
        private int _GanhoMaximo;
        private ModoTeste _ModoTeste;
        private int _NivelImagemDesejada;
        private int _NivelImagemAtual;
        private int _GanhoAtual;
        private int _ShutterAtual;
        private int _Reservado;
        private int _GanhoEstendido;
        private FormatoImagem _FormatoImagem;
        private int _QualidadeJPEG;

        public int QualidadeJPEG
        {
            get { return _QualidadeJPEG; }
            set { _QualidadeJPEG = value; }
        }

        public FormatoImagem FormatoImagem
        {
            get { return _FormatoImagem; }
            set { _FormatoImagem = value; }
        }

        public int GanhoEstendido
        {
            get { return _GanhoEstendido; }
            set { _GanhoEstendido = value; }
        }

        public int Reservado
        {
            get { return _Reservado; }
            set { _Reservado = value; }
        }

        public int ShutterAtual
        {
            get { return _ShutterAtual; }
            set { _ShutterAtual = value; }
        }

        public int GanhoAtual
        {
            get { return _GanhoAtual; }
            set { _GanhoAtual = value; }
        }

        public int NivelImagemAtual
        {
            get { return _NivelImagemAtual; }
            set { _NivelImagemAtual = value; }
        }

        public int NivelImagemDesejada
        {
            get { return _NivelImagemDesejada; }
            set { _NivelImagemDesejada = value; }
        }

        public ModoTeste ModoTeste
        {
            get { return _ModoTeste; }
            set { _ModoTeste = value; }
        }

        public int GanhoMaximo
        {
            get { return _GanhoMaximo; }
            set { _GanhoMaximo = value; }
        }

        public int ValorGanho
        {
            get { return _ValorGanho; }
            set { _ValorGanho = value; }
        }

        public TipoGanho TipoGanho
        {
            get { return _TipoGanho; }
            set { _TipoGanho = value; }
        }

        public TipoGanhoEstendido TipoGanhoEstendido
        {
            get { return _TipoGanhoEstendido; }
            set { _TipoGanhoEstendido = value; }
        }

        public int ShutterMaximo
        {
            get { return _ShutterMaximo; }
            set { _ShutterMaximo = value; }
        }

        public int ValorShutter
        {
            get { return _ValorShutter; }
            set { _ValorShutter = value; }
        }

        public TipoShutter Shutter
        {
            get { return _Shutter; }
            set { _Shutter = value; }
        }

        public TipoContraste Contraste
        {
            get { return _Contraste; }
            set { _Contraste = value; }
        }
    }
}
