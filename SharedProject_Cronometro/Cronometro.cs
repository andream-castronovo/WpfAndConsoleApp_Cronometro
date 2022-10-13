using System;

namespace WpfAndConsoleApp_Cronometro
{
    internal class Cronometro
    {
        // Classe programmata da Andrea Maria Castronovo iniziata il 09/10/2022

        #region CDC
        DateTime _dataInizio = DateTime.Now; // Entrambe alla data attuale così in caso l'utente non avvi
        DateTime _dataFine = DateTime.Now;   // mai il cronometro la sottrazione sarà 0

        bool _started; // Cronometro in corso o no
        #endregion

        #region Costruttori
        /// <summary>
        /// Costruttore
        /// </summary>
        /// <param name="tempo">Tempo di inizio cronometro. Formato: "HH:MM:SS"</param>
        public Cronometro(string tempo)
        {
            _dataInizio = OttieniDataDaTempo(tempo);
        }
        /// <summary>
        /// Cronometro con inizio di default: 00:00:00
        /// </summary>
        public Cronometro() : this("00:00:00")
        {
        }
        #endregion

        #region Reset
        /// <summary>
        /// Reset cronometro a 00:00:00
        /// </summary>
        public void Reset()
        {
            // Entrambe alla data attuale così in caso l'utente non avvi
            // mai il cronometro la sottrazione sarà 0
            _dataInizio = DateTime.Now;
            _dataFine = DateTime.Now;
        }

        /// <summary>
        /// Imposta il cronometro ad un tempo preciso
        /// </summary>
        /// <param name="tempo">Formato "HH:MM:SS"</param>
        public void Reset(string tempo)
        {
            _dataInizio = OttieniDataDaTempo(tempo);
            _dataFine = DateTime.Now; // Se l'utente non avvia mai il cronometro, darà sempre lo stesso tempo
        }
        #endregion

        /// <summary>
        /// Avvia il cronometro da dove ha lasciato
        /// </summary>
        /// <exception cref="Exception">Cronometro già avviato</exception>
        public void Start()
        {
            if (_started)
                throw new Exception("Cronometro già avviato");

            _started = true;

            
            TimeSpan ts = _dataFine - _dataInizio; // TS è la differenza di tempo tra la fine e l'inizio del cronometro
            _dataInizio = DateTime.Now-ts; // La data di adesso, meno la differenza di tempo tra la fine e l'inizio di prima,
                                           // mi darà lo stesso tempo di differenza di prima
                                           // Esempio:
                                           // Se ho iniziato il cronometro alle 15:33:42, l'ho fermato alle 15:33:56, tra loro ci sono 14 secondi di differenza;
                                           // Tra la data di ora, e la data di 14 secondi fa, c'è una differenzad di 14 secondi,
                                           // quindi posso ricominciare a contare da qui.
        }

        /// <summary>
        /// Ferma il cronometro ma non lo resetta
        /// </summary>
        /// <exception cref="Exception">Cronometro non avviato</exception>
        public void Stop()
        {
            if (!_started)
                throw new Exception("Cronometro non avviato");

            _started = false;
            _dataFine = DateTime.Now; // Per poter leggere il tempo anche se il cronometro è fermo
        }

        /// <summary>
        /// Restituisce il tempo del cronometro sotto forma di stringa
        /// </summary>
        /// <returns>string: Tempo del cronometro</returns>
        public string LeggiTempo()
        {
            DateTime data = DateTime.Now;

            if (!_started)
                data = _dataFine; // Per poter leggere il tempo anche se il cronometro è fermo

            // Aggiungo uno 0 se qualcosa è minore di 10
            string formattaOre = (data - _dataInizio).Hours < 10 ? "0" : "";
            string formattaMinuti = (data - _dataInizio).Minutes < 10 ? "0" : "";
            string formattaSecondi = (data - _dataInizio).Seconds < 10 ? "0" : "";
            string formattaGiorni = (data - _dataInizio).Days < 10 ? "0" : "";

            string giorni = (data - _dataInizio).Days > 0 ? formattaGiorni + (data - _dataInizio).Days + ":" : "";
            string ore = formattaOre + (data - _dataInizio).Hours;
            string minuti = formattaMinuti + (data - _dataInizio).Minutes;
            string secondi = formattaSecondi + (data - _dataInizio).Seconds;

            return $"{giorni}{ore}:{minuti}:{secondi}";
        }


        /// <summary>
        /// Restituisce il tempo intero in modo da permettere usi generali
        /// </summary>
        /// <returns>TimeSpan: Tempo calcolato dal cronometro</returns>
        public TimeSpan OttieniTempo()
        {
            return _started ? DateTime.Now - _dataInizio : _dataFine - _dataFine;
        }


        /// <summary>
        /// Ottieni la data che sottratta ad oggi da il tempo scelto
        /// </summary>
        /// <param name="tempo">Tempo in formato HH:MM:SS</param>
        /// <returns>DateTime: Data che sottratta ad oggi da il tempo scelto</returns>
        /// <exception cref="Exception">Tempo in formato non corretto</exception>
        private DateTime OttieniDataDaTempo(string tempo)
        {
            string[] tempi = tempo.Split(':');

            TimeSpan ts;

            switch (tempi.Length)
            {
                case 1:
                    ts = new TimeSpan(0,0,int.Parse(tempi[0]));
                    break;
                case 2:
                    ts = new TimeSpan(0,int.Parse(tempi[0]),int.Parse(tempi[1]));
                    break;
                case 3:
                    ts = new TimeSpan(int.Parse(tempi[0]), int.Parse(tempi[1]), int.Parse(tempi[2]));
                    break;
                case 4:
                    ts = new TimeSpan(int.Parse(tempi[0]), int.Parse(tempi[1]), int.Parse(tempi[2]), int.Parse(tempi[3]));
                    break;
                default:
                    throw new Exception("Non hai inserito i tre valori correttamente");

            }

            // La data di ora, meno il tempo scelto e convertito in TimeSpan
            // ci darà la data che se sottratta alla data attuale darà quel tempo

            return DateTime.Now - ts;

        }
    }
}