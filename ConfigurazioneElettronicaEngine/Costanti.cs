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
    /// Raccolta di costanti e tabelle varie
    /// </summary>
    public static class Costanti
    {
        /// <summary>
        /// Numero atomico iniziale di ogni periodo
        /// </summary>
        public static int[] Periodi = { 1, 3, 11, 19, 37, 55, 87 };

        /// <summary>
        /// Famiglie di elementi con caratteristiche simili
        /// </summary>
        public enum Famiglie
        {
            Metalli_Alcalini, Metalli_AlcalinoTerrosi,
            Metalli_Blocco_d, Metalli_Blocco_p, SemiMetalli, 
            Non_Metalli, Alogeni, Gas_Nobili,
            Lantanoidi, Attinoidi, Non_Definito
        }
    }
}
