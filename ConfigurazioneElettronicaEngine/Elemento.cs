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
    public record Elemento(int NumeroAtomico, string Simbolo, string Nome)
    {
        /// <summary>
        /// Costruisce il record a partire da una riga di CSV
        /// </summary>
        /// <param name="csvRecord"></param>
        public Elemento(string csvRecord) : this(0, "", "")
        {
            string[] fields = csvRecord.Split(";");
            NumeroAtomico = int.Parse(fields[0]);
            Nome = fields[1];
            Simbolo = fields[2];
        }
    }
}
