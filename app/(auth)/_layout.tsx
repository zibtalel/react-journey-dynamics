
import { Stack } from 'expo-router';
import { View, StyleSheet } from 'react-native';
import ThemedStatusBar from '../../src/components/ThemedStatusBar';
import ConnectivityBar from '../../src/components/navigation/ConnectivityBar';
import { useThemeColors } from '../../src/hooks/useThemeColors';

export default function AuthLayout() {
  const colors = useThemeColors();

  return (
    <View style={[styles.container, { backgroundColor: colors.background }]}>
      <ThemedStatusBar translucent />
      <ConnectivityBar />
      <View style={styles.contentContainer}>
        <Stack screenOptions={{ headerShown: false }}>
          <Stack.Screen name="login" />
          <Stack.Screen name="register" />
        </Stack>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  contentContainer: {
    flex: 1,
    marginTop: 30, // Reduced space for connectivity bar
  }
});
