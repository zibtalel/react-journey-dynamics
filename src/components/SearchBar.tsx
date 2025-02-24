
import { Search } from "lucide-react";
import { Input } from "@/components/ui/input";

interface SearchBarProps {
  onSearch: (value: string) => void;
}

export function SearchBar({ onSearch }: SearchBarProps) {
  return (
    <div className="relative w-full max-w-xl">
      <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-muted-foreground w-4 h-4" />
      <Input
        className="pl-10 bg-white/50 backdrop-blur-sm"
        placeholder="Search jobs..."
        onChange={(e) => onSearch(e.target.value)}
      />
    </div>
  );
}
