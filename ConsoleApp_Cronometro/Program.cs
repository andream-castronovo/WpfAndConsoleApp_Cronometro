using System;

namespace WpfAndConsoleApp_Cronometro
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Programmato da Andrea Maria Castronovo iniziata il 09/10/2022
            // Comandi nel metodo "Intestazione()"
            
            Cronometro crn = new Cronometro(); // Istanza del Cronometro

            // Intestazione e comandi
            Console.WriteLine("Programmato da Andrea Maria Castronovo - 4°I - 09/10/2022");
            Comandi();

            bool condizione = true;
            
            while (condizione)
            {
                char tastoPremuto = Console.ReadKey(true).KeyChar;
                try
                {
                    switch (tastoPremuto.ToString().ToLower()) // Minuscolo così funziona anche con SHIFT
                    {
                        case "s": // Start
                            crn.Start();
                            ScriviInUnColore("Cronometro avviato", ConsoleColor.Green);
                            break;
                        case "r": // Reset
                            crn.Reset();
                            ScriviInUnColore("Cronometro riavviato", ConsoleColor.Green);
                            break;
                        case "q": // Quit
                            condizione = false;
                            break;
                        case "l": // Leggi
                            Console.WriteLine(crn.LeggiTempo());
                            break;
                        case "f": // Ferma
                            crn.Stop();
                            ScriviInUnColore("Cronometro fermato", ConsoleColor.Green);
                            break;
                        case "p": // Pulisci console
                            Console.Clear();
                            Comandi();
                            break;
                        case "t": // Resetta con tempo
                            ImpostaTempo();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    ScriviInUnColore(ex.Message,ConsoleColor.Red); // Notifica errore
                }
            }

            Console.WriteLine("\n\n\tProgramma terminato, premi invio per chiudere");
            Console.ReadLine();

            #region Metodi
            void Comandi()
            {
                Console.WriteLine("Comandi cronometro: ");
                Console.WriteLine("S = Start");
                Console.WriteLine("R = Reset");
                Console.WriteLine("Q = Quit");
                Console.WriteLine("L = Leggi");
                Console.WriteLine("F = Ferma");
                Console.WriteLine("P = Pulisci");
                Console.WriteLine("T = Avvia/Riavvia con tempo");
                Console.WriteLine("");
            }

            void ScriviInUnColore(string messaggio, ConsoleColor colore, bool newLine = true)
            {
                Console.ForegroundColor = colore;

                if (newLine)
                    Console.WriteLine(messaggio);
                else
                    Console.Write(messaggio);

                Console.ForegroundColor = ConsoleColor.Gray;
            }


            void ImpostaTempo()
            {
                bool inOk = true;
                do
                {
                    Console.WriteLine("Inserisci tempo di inizio, formato DD:HH:MM:SS");
                    string tempo = Console.ReadLine();
                    try
                    {
                        crn.Reset(tempo);
                        Console.Write("Tempo di inizio impostato a ");
                        ScriviInUnColore(tempo, ConsoleColor.Green, false);
                        Console.WriteLine(" con successo");
                    }
                    catch (Exception ex)
                    {
                        inOk = false;
                        ScriviInUnColore(ex.Message, ConsoleColor.Red);
                    }
                } while (!inOk);
            }
            #endregion

        }
    }
}
