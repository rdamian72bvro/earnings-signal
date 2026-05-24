export interface LiveSignal {
  ticker: string;
  companyName: string;
  sector: string;
  signalType: string;
  score: number;
  reasonSummary: string;
  generatedAtUtc: string;
}
