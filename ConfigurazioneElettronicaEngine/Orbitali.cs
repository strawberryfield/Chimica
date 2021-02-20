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

using System.Collections.Generic;
using System.Linq;

namespace Casasoft.ConfigurazioneElettronica
{
    /// <summary>
    /// La classe contiene una lista (privata) di tutti i possibili orbitali
    /// con indicazioni per l'ordine di riempimento
    /// </summary>
    public class Orbitali
    {
        private List<Orbitale> Livelli;

        /// <summary>
        /// Costruttore
        /// </summary>
        /// <remarks>
        /// Crea e popola la lista privata di orbitali
        /// assegnando anche l'ordine di riempimento
        /// e quello di stampa
        /// </remarks>
        public Orbitali()
        {
            Livelli = new();

            // creazione livelli
            int PrintOrder = 1;
            for (int n = 1; n <= 7; n++)
            {
                for (Sottolivelli s = Sottolivelli.s; s <= Sottolivelli.f && (int)s < n; s++)
                {
                    if (n >= 6 && s >= Sottolivelli.f) continue;
                    if (n >= 7 && s >= Sottolivelli.d) continue;

                    Orbitale o = new(n, s);
                    o.PrintOrder = PrintOrder;
                    Livelli.Add(o);
                    PrintOrder++;
                }
            }

            // Assegnazione ordine di riempimento
            int FillOrder = 1;
            for (Sottolivelli s = Sottolivelli.s; s <= Sottolivelli.f; s++)
            {
                Sottolivelli sc = s;
                for (int n = 1; n <= (int)s + 1; n++)
                {
                    Orbitale orb = Livelli.Find(o => o.N == n && o.Sottolivello == sc);
                    if (orb != null)
                    {
                        orb.FillOrder = FillOrder;
                        FillOrder++;
                    }
                    sc--;
                }
            }

            for (int n = 2; n <= 5; n++)
            {
                int nc = n;
                for (Sottolivelli s = Sottolivelli.f; s >= Sottolivelli.s; s--)
                {
                    Orbitale orb = Livelli.Find(o => o.N == nc && o.Sottolivello == s);
                    if (orb != null)
                    {
                        orb.FillOrder = FillOrder;
                        FillOrder++;
                    }
                    nc++;

                }
            }
        }

        /// <summary>
        /// Azzera eventuali calcoli di configurazione precedenti
        /// </summary>
        /// <remarks>
        /// Generalmente non serve chiamarla in quanto il metodo
        /// <see cref="Calcola(int)"/> provvede da solo all'azzeramento
        /// </remarks>
        public void Clear()
        {
            foreach (Orbitale o in Livelli)
                o.Clear();
        }

        /// <summary>
        /// Effettua il calcolo della configurazione
        /// </summary>
        /// <param name="n">Numero atomico</param>
        public void Calcola(int n)
        {
            Clear();
            if (!Accettabile(n)) return;

            int resto = n;
            List<Orbitale> FillList = Livelli.OrderBy(o => o.FillOrder).ToList();
            foreach (Orbitale o in FillList)
            {
                resto = o.TryFill(resto);
                if (resto == 0) break;
            }
        }

        /// <summary>
        /// Verifica se il numero atomico è valido
        /// </summary>
        /// <param name="n">numero da controllare</param>
        /// <returns>true se il numero è valido</returns>
        public static bool Accettabile(int n) => n > 0 && n <= 118;

        /// <summary>
        /// Stampa la configurazione
        /// </summary>
        /// <returns>stringa contenente la configurazione in sigle</returns>
        public string Print()
        {
            List<Orbitale> PrintList = Livelli.OrderBy(o => o.PrintOrder).ToList();
            string ret = string.Empty;
            foreach (Orbitale o in PrintList)
                ret += o.Print();

            return ret;
        }

        /// <summary>
        /// Stampa lo schema di configurazione
        /// </summary>
        /// <returns></returns>
        public string PrintSchema()
        {
            string ret = string.Empty;
            for (int l = 7; l > 0; l--)
            {
                ret += PrintSchemaLivello(l);
            }
            return ret;
        }

        /// <summary>
        /// Stampa lo schema di un singolo livello
        /// </summary>
        /// <param name="l">livello da stampare</param>
        /// <returns></returns>
        private string PrintSchemaLivello(int l)
        {
            string ret = string.Empty;
            List<Orbitale> livello = Livelli.FindAll(lo => lo.N == l).OrderBy(o => o.Sottolivello).ToList();
            decimal totRiempimento = livello.Sum(o => o.Riempimento);
            if (totRiempimento == 0) return ret;

            string line = LivelloTopBottomLine(livello);
            ret += line;
            ret += LivelloDataLine(livello);
            ret += line;
            return ret;
        }

        /// <summary>
        /// Prepara le linee del livello
        /// </summary>
        /// <param name="livello"></param>
        /// <returns></returns>
        private string LivelloTopBottomLine(List<Orbitale> livello)
        {
            string ret = " ";
            foreach (Orbitale o in livello)
                ret += " " + o.TopBottomLine();
            return $"{ret}\n";
        }

        private string LivelloDataLine(List<Orbitale> livello)
        {
            string ret = livello[0].N.ToString();
            foreach (Orbitale o in livello)
                ret += " " + o.PrintSchema();
            return $"{ret}\n";
        }

    }

}
