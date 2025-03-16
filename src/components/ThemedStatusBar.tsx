
import { StatusBar, StatusBarProps } from 'expo-status-bar';
import { useTheme } from '../contexts/ThemeContext';

interface ThemedStatusBarProps extends Omit<StatusBarProps, 'style'> {
  translucent?: boolean;
}

export default function ThemedStatusBar({ translucent = true, ...props }: ThemedStatusBarProps) {
  const { theme } = useTheme();
  
  return (
    <StatusBar 
      style={theme === 'dark' ? 'light' : 'dark'} 
      translucent={translucent}
      {...props}
    />
  );
}
