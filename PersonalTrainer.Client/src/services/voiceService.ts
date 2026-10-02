const isSupported = () => 'speechSynthesis' in window;

const speak = (text: string, options?: { rate?: number; pitch?: number; volume?: number }): Promise<void> => {
  return new Promise(resolve => {
    if (!isSupported()) { resolve(); return; }

    window.speechSynthesis.cancel();

    const utterance = new SpeechSynthesisUtterance(text);
    utterance.rate   = options?.rate   ?? 1;
    utterance.pitch  = options?.pitch  ?? 1;
    utterance.volume = options?.volume ?? 1;
    utterance.onend  = () => resolve();

    window.speechSynthesis.speak(utterance);
  });
};

const stop = (): void => {
  if (!isSupported()) return;
  window.speechSynthesis.cancel();
};

export const voiceService = { speak, stop, isSupported };
