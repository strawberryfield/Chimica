// copyright (c) 2021 Roberto Ceccarelli - Casasoft
// http://strawberryfield.altervista.org 
// 
// This is free software: 
// you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
// See the GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  
// If not, see <http://www.gnu.org/licenses/>.

namespace Casasoft.ConfigurazioneElettronica
{
    /// <summary>
    /// Informazioni sull'elemento chimico
    /// </summary>
    public record Elemento
    {
        private int _periodo;
        private int _gruppo;

        public virtual int NumeroAtomico { get; init; }
        public virtual string Simbolo { get; init; }
        public virtual string Nome { get; init; }

        /// <summary>
        /// Costruttore standard
        /// </summary>
        /// <param name="numeroAtomico"></param>
        /// <param name="simbolo"></param>
        /// <param name="nome"></param>
        public Elemento(int numeroAtomico, string simbolo, string nome)
        {
            NumeroAtomico = numeroAtomico;
            Nome = nome;
            Simbolo = simbolo;

            _periodo = getPeriodo();
            _gruppo = getGruppo();
        }

        /// <summary>
        /// Costruisce il record a partire da una riga di CSV
        /// </summary>
        /// <param name="csvRecord"></param>
        public Elemento(string csvRecord)
        {
            string[] fields = csvRecord.Split(";");
            NumeroAtomico = int.Parse(fields[0]);
            Nome = fields[1];
            Simbolo = fields[2];

            _periodo = getPeriodo();
            _gruppo = getGruppo();
        }

        public int Periodo => _periodo;
        public int Gruppo => _gruppo;

        private int[] periodi = { 1, 3, 11, 19, 37, 55, 87 };

        /// <summary>
        /// Calcola il periodo
        /// </summary>
        /// <returns></returns>
        private int getPeriodo()
        {
            int ret = periodi.Length - 1;
            for (; ret >= 0; ret--)
            {
                if (NumeroAtomico >= periodi[ret]) break;
            }
            return ret + 1;
        }

        /// <summary>
        /// Calcola il gruppo
        /// </summary>
        /// <returns></returns>
        private int getGruppo()
        {
            int ret = 0;
            if (Periodo == 1)
            {
                ret = NumeroAtomico == 1 ? 1 : 18;
            }
            else if (Periodo == 2 || Periodo == 3)
            {
                ret = NumeroAtomico - periodi[Periodo - 1] + (NumeroAtomico <= periodi[Periodo - 1] + 1 ? 1 : 11);
            }
            else if (Periodo == 4 || Periodo == 5)
            {
                ret = NumeroAtomico - periodi[Periodo - 1] + 1;
            }
            else
            {
                ret = NumeroAtomico - periodi[Periodo - 1] + 1;
                if (ret >= 4 && ret <= 17) ret = 0;
                else if (ret > 17) ret -= 14;
            }
            return ret;
        }
    }
}
