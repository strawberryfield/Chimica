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
using System.IO;

namespace Casasoft.ConfigurazioneElettronica
{
    /// <summary>
    /// Lista degli elementi chimici
    /// </summary>
    public class Elementi
    {
        private List<Elemento> elementi;

        /// <summary>
        /// Costruisce la lista da un file CSV
        /// </summary>
        public Elementi()
        {
            elementi = new();
            string[] righe = File.ReadAllLines("Elementi.csv");
            for (int i = 1; i < righe.Length; i++)
            {
                elementi.Add(new(righe[i]));
            }
        }

        /// <summary>
        /// Trova l'elemento in base al numero atomico
        /// </summary>
        /// <param name="n">numero atomico</param>
        /// <returns>null se non trovato</returns>
        public Elemento TrovaElementoPerNumero(int n) => elementi.Find(e => e.NumeroAtomico == n);

        /// <summary>
        /// Trova l'elemento in base al simbolo
        /// </summary>
        /// <param name="s">Simbolo</param>
        /// <returns>null se non trovato</returns>
        public Elemento TrovaElementoPerSimbolo(string s) => elementi.Find(e => e.Simbolo == s);

        /// <summary>
        /// Trova l'elemento in base al nome
        /// </summary>
        /// <param name="s">Nome</param>
        /// <returns>null se non trovato</returns>
        public Elemento TrovaElementoPerNome(string s) => elementi.Find(e => e.Nome == s);
    }
}
