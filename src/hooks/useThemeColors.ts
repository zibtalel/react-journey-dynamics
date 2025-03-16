
import { useTheme } from '../contexts/ThemeContext';
import { getThemeColors } from '../styles/theme';
import { wp } from '../utils/responsive';

export const useThemeColors = () => {
  const { theme } = useTheme();
  const colors = getThemeColors(theme);
  
  // Add responsive sizing utilities to the colors object
  return {
    ...colors,
    spacing: {
      xs: wp(4),
      sm: wp(8),
      md: wp(16),
      lg: wp(24),
      xl: wp(32),
    },
    fontSize: {
      xs: wp(10),
      sm: wp(12),
      base: wp(14),
      md: wp(16),
      lg: wp(18),
      xl: wp(24),
      xxl: wp(32),
    },
    borderRadius: {
      sm: wp(4),
      md: wp(8),
      lg: wp(16),
      full: 9999,
    },
    // Additional utility for creating translucent colors
    opacity: (color: string, alpha: number): string => {
      // Convert alpha (0-1) to hex (00-FF)
      const hex = Math.round(alpha * 255).toString(16).padStart(2, '0');
      return `${color}${hex}`;
    },
    // Map specific styles
    map: {
      controlButtonsTop: Platform.OS === 'ios' ? height * 0.35 : height * 0.32, // Positioned further down
      userLocationBg: theme === 'dark' ? '#60A5FA' : '#0EA5E9',
      userLocationBorder: theme === 'dark' ? '#FFFFFF' : '#FFFFFF',
      actionButtonBg: `${theme === 'dark' ? '#1E293B' : '#FFFFFF'}CC`, // Semi-transparent
    }
  };
};
