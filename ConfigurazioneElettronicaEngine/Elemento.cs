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

using System.Linq;

namespace Casasoft.ConfigurazioneElettronica
{
    /// <summary>
    /// Informazioni sull'elemento chimico
    /// </summary>
    public record Elemento
    {
        private int _periodo;
        private int _gruppo;
        private Costanti.Famiglie _famiglia;

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
            init();        }

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
            init();
        }

        /// <summary>
        /// Perido (riga) della tavola degli elementi
        /// </summary>
        public int Periodo => _periodo;

        /// <summary>
        /// Gruppo (colonna) della tavola degli elementi
        /// </summary>
        public int Gruppo => _gruppo;

        /// <summary>
        /// Famiglia (o classe) di elementi aventi caratteristiche simili
        /// </summary>
        public Costanti.Famiglie Famiglia => _famiglia;

        private void init()
        {
            _periodo = getPeriodo();
            _gruppo = getGruppo();
            _famiglia = getFamiglia();
        }

        /// <summary>
        /// Calcola il periodo
        /// </summary>
        /// <returns></returns>
        private int getPeriodo()
        {
            int ret = Costanti.Periodi.Length - 1;
            for (; ret >= 0; ret--)
            {
                if (NumeroAtomico >= Costanti.Periodi[ret]) break;
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
                ret = NumeroAtomico - Costanti.Periodi[Periodo - 1] + (NumeroAtomico <= Costanti.Periodi[Periodo - 1] + 1 ? 1 : 11);
            }
            else if (Periodo == 4 || Periodo == 5)
            {
                ret = NumeroAtomico - Costanti.Periodi[Periodo - 1] + 1;
            }
            else
            {
                ret = NumeroAtomico - Costanti.Periodi[Periodo - 1] + 1;
                if (ret >= 3 && ret <= 17) ret = 0;
                else if (ret > 17) ret -= 14;
            }
            return ret;
        }

        /// <summary>
        /// Calcola la famiglia
        /// </summary>
        /// <returns></returns>
        private Costanti.Famiglie getFamiglia()
        {
            Costanti.Famiglie ret = Costanti.Famiglie.Non_Definito;

            if (NumeroAtomico >= 113)
                ret = Costanti.Famiglie.Non_Definito;
            else if (new[] { 1, 6, 7, 8, 15, 16, 34 }.Contains(NumeroAtomico))
                ret = Costanti.Famiglie.Non_Metalli;
            else if (new[] { 13, 31, 49, 50, 81, 82, 83 }.Contains(NumeroAtomico))
                ret = Costanti.Famiglie.Metalli_Blocco_p;
            else if (Gruppo == 1)
                ret = Costanti.Famiglie.Metalli_Alcalini;
            else if (Gruppo == 2)
                ret = Costanti.Famiglie.Metalli_AlcalinoTerrosi;
            else if (Gruppo >= 3 && Gruppo <= 12)
                ret = Costanti.Famiglie.Metalli_Blocco_d;
            else if (Gruppo == 0 && Periodo == 6)
                ret = Costanti.Famiglie.Lantanoidi;
            else if (Gruppo == 0 && Periodo == 7)
                ret = Costanti.Famiglie.Attinoidi;
            else if (Gruppo == 18)
                ret = Costanti.Famiglie.Gas_Nobili;
            else if (Gruppo == 17 && Periodo < 6)
                ret = Costanti.Famiglie.Alogeni;
            else
                ret = Costanti.Famiglie.SemiMetalli;

            return ret;
        }
    }
}
