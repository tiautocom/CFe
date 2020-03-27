using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegraNegocio
{
   public class ImagemRegraNegocio
    {
       AcessoDados.ImagemAcessoDados novaImagem;
        public void LimparImagem()
        {
            try
            {
                novaImagem = new AcessoDados.ImagemAcessoDados();
                novaImagem.LimparImagem();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void SalvarImagem(byte[] foto)
        {
            try
            {
                novaImagem = new AcessoDados.ImagemAcessoDados();
                novaImagem.SalvarImagem(foto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
