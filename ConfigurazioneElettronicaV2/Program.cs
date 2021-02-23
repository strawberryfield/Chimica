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

using System;
using Casasoft.ConfigurazioneElettronica;

// Inizializza il business object
Orbitali orbitali = new();
Elementi elementi = new();

#region --- User interface ---
Console.WriteLine("\tCONFIGURAZIONE ELETTRONICA");
Console.WriteLine("\t--------------------------\n");

// Ciclo di gestione 
while (true)
{
    Console.Write("Numero atomico: ");
    string sNum = Console.ReadLine();

    // Viene convertita la stringa in un numero intero, gestendo gli eventuali errori
    int Num;
    if (!int.TryParse(sNum, out Num))
    {
        Console.WriteLine("Numero non riconosciuto.");
        continue;
    }

    // Se viene inserito 0 il programma termina
    if (Num == 0)
    {
        Console.WriteLine("Arrivederci.");
        break;
    }

    // Chiediamo al business object se il numero è elaborabile
    if (!Orbitali.Accettabile(Num))
    {
        Console.WriteLine("Numero non configurabile.");
        continue;
    }

    // Se tutti i controlli precedenti sono andati a buon fine 
    // calcoliamo la configurazione e la stampiamo
    Elemento e = elementi.TrovaElementoPerNumero(Num);
    Console.WriteLine($"{e.Simbolo} {e.Nome}");
    Console.WriteLine($"Periodo: {e.Periodo}, Gruppo: {e.Gruppo}, Famiglia: {e.Famiglia.ToString().Replace('_', ' ')}");
    orbitali.Calcola(Num);
    Console.WriteLine(orbitali.Print());
    Console.WriteLine();
    Console.WriteLine(orbitali.PrintSchema());
}
#endregion
