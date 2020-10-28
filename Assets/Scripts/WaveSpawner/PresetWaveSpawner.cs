public class PresetWaveSpawner : WaveSpawner {
    public PresetWaveContents[] waves;


    public override int MaxWave => waves.Length - 1;

    protected override WaveContents GetWaveContents(int w) {
        if (w >= waves.Length) {
            return null;
        }

        return waves[w];
    }
}