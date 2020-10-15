public class PresetWaveSpawner : WaveSpawner {
    public PresetWaveContents[] waves;

    protected override WaveContents GetWaveContents(int w) {
        if (w >= waves.Length) {
            return null;
        }

        return waves[w];
    }
}