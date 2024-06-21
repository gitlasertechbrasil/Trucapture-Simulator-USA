using System;

namespace TruCaptureEmulator
{
    public enum TipoDisparoFlash
    {
        DESATIVADO=1,
        UNICO=2,
        CONTINUO=3,
        UNICO_COM_DELAY=4,
        AUTOMATICO=5,
        AUTOMATICO_COM_DELAY=6,
        //DESCONECTADO = 7
    }

    public enum TipoRequisicaoFoto
    {
        REDE=1,
        ENTRADA_IO=2,
        //DESCONECTADO = 3
    }

    public enum TipoSaidaCamera
    {
        FLASH=1,
        IO=2,
        //DESCONECTADO = 3
    }

    public enum NivelSinal
    {
        DESACIONADO=0,
        ACIONADO=1,
        //DESCONECTADO = 2,
    }

    public enum TipoContraste
    {
        NORMAL=0,
        HDR=1,
        //DESCONECTADO = 2
    }

    public enum TipoShutter
    {
        FIXO=0,
        AUTOMATICO=1,
        FIXO_DAY_AUT_NIGHT = 2,
        //DESCONECTADO = 3
    }

    public enum TipoGanho
    {
        FIXO=0,
        AUTOMATICO=1,
        //DESCONECTADO = 2
    }

    public enum TipoGanhoEstendido
    {
        DESABILITADO = 0,
        HABILITADO_MODO_DAY = 1,
        HABILITADO_MODO_NIGHT = 2,
        //DESCONECTADO = 3
    }

    public enum ModoTeste
    {
        DESABILITADO=0,
        VERTICAL=1,
        HORIZONTAL=2,
        DIAGONAL=3,
        //DESCONECTADO =3
    }

    public enum FormatoImagem
    {
        BMP=0,
        JPG=1,
        //DESCONECTADO = 2
    }

    public enum ModoOperacao
    {
        AUTOMATICO = 0,
        DAY = 1,
        NIGHT = 2,
        //DESCONECTADO = 3
    } 
  
    public enum ModoOCR
    {
        DESABILITADO = 0,
        RAPIDO = 1,
        NORMAL = 2,
        LENTO = 3,
        MUITOLENTO = 4
        //0: OCR desabilitado/ 1: OCR rápido/ 2: OCR normal/ 3: OCR lento/ 4: OCR muito lento
    }

   

    public enum ModoOCRDif
    {
        DESABILITADO = 0,
        DIFERENCIADODAY = 1,
        DIFERENCIADONIGHT = 2
    }
}
