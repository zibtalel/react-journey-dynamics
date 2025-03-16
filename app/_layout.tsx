
import { Stack } from 'expo-router';
import { useEffect } from 'react';
import { router } from 'expo-router';
import { ThemeProvider } from '../src/contexts/ThemeContext';
import ThemedStatusBar from '../src/components/ThemedStatusBar';
import { View, StyleSheet } from 'react-native';

export default function RootLayout() {
  // This effect will run once when the app starts
  useEffect(() => {
    // Redirect to login screen when the app starts
    router.replace('/(auth)/login');
  }, []);

  return (
    <ThemeProvider>
      <View style={styles.container}>
        <ThemedStatusBar translucent />
        <Stack screenOptions={{ headerShown: false }}>
          <Stack.Screen name="(auth)" />
          <Stack.Screen name="(tabs)" />
          <Stack.Screen 
            name="message/[id]" 
            options={{ 
              presentation: 'modal',
              animation: 'slide_from_right'
            }} 
          />
          <Stack.Screen
            name="messages/[id]"
            options={{
              presentation: 'modal',
              animation: 'slide_from_right'
            }}
          />
        </Stack>
      </View>
    </ThemeProvider>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
});
