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
    /// Denominazione dei numeri quantici secondari
    /// </summary>
    public enum Sottolivelli { s, p, d, f }

    /// <summary>
    /// Classe che contiene la struttura di ogni orbitale
    /// </summary>
    public class Orbitale
    {
        /// <summary>
        /// Numero quantico principale
        /// </summary>
        public int N { get; init; }

        /// <summary>
        /// Numero quantico secondario
        /// </summary>
        public Sottolivelli Sottolivello { get; init; }

        /// <summary>
        /// Numero massimo di elettroni che possono essere presenti in questo livello
        /// </summary>
        public int Capacita => ((int)Sottolivello * 2 + 1) * 2;

        /// <summary>
        /// Utilizzata durante il calcolo
        /// </summary>
        public int Riempimento { get; private set; }

        /// <summary>
        /// Ordine per stampa
        /// </summary>
        public int PrintOrder { get; set; }
        /// <summary>
        /// Ordine per il riempimento
        /// </summary>
        public int FillOrder { get; set; }

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="n">Numero quantico principale</param>
        /// <param name="s">Numero quantico secondario</param>
        public Orbitale(int n, Sottolivelli s)
        {
            N = n;
            Sottolivello = s;
            Clear();
            FillOrder = int.MaxValue;
            PrintOrder = int.MaxValue;
        }

        /// <summary>
        /// Azzera il riempimento
        /// </summary>
        public void Clear() => Riempimento = 0;

        /// <summary>
        /// Assegna al livello quanti più elettroni possibile
        /// scalandoli dal parametro passato
        /// </summary>
        /// <param name="n">Numero di elettroni da collocare</param>
        /// <returns>numero degli elettroni residui dopo aver riempito il livello</returns>
        public int TryFill(int n)
        {
            int ret = 0;
            if (Capacita >= n)
            {
                Riempimento = n;
            }
            else
            {
                Riempimento = Capacita;
                ret = n - Capacita;
            }
            return ret;
        }

        /// <summary>
        /// Stampa la configurazione in sigla
        /// </summary>
        /// <returns>stringa contenente la sigla di configurazione</returns>
        public string Print() => Riempimento > 0 ? $"{N}{Sottolivello}{Riempimento} " : string.Empty;

        /// <summary>
        /// Linea per lo schema della configurazione
        /// </summary>
        /// <returns></returns>
        public string TopBottomLine()
        {
            string ret = "+";
            for (int i = 0; i < Capacita / 2; i++)
            {
                ret += "--+";
            }
            return ret;
        }

        /// <summary>
        /// Stampa gli elettroni presenti nell'orbitale
        /// </summary>
        /// <returns></returns>
        public string PrintSchema()
        {
            string ret = "|";
            int sl = Capacita / 2;
            for (int i = 0; i < sl; i++)
            {
                ret += (i < Riempimento ? "^" : " ");
                ret += (i + sl < Riempimento ? "v" : " ");
                ret += "|";
            }
            return ret;
        }
    }

}
