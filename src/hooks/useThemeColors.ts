
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
    }
  };
};
